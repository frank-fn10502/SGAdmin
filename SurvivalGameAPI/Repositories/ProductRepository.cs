using AutoMapper;
using Dapper;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using SurvivalGameAPI.CustomModels.DTOModel;
using SurvivalGameAPI.CustomModels.ViewModel;
using SurvivalGameAPI.CustomModels.ViewModel.Product;
using SurvivalGameAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.Repositories
{
    public class ProductRepository
    {
        //private SGModel _context = new SGModel();
        private SGModel _context = new SGModel();
        private string _conn = ConfigurationManager.ConnectionStrings["SGModel"].ConnectionString;
        public IEnumerable<SimpleProductViewModel> GetAllSimpleProduct()
        {
            IEnumerable<SimpleProductViewModel> result = new List<SimpleProductViewModel>();
            using (var connection = new SqlConnection(_conn))
            {
                var sql =
                        @"select p.ID ,
                            (	
	                            select Name
	                            from Class as cl
	                            where cl.ID = p.ClassID
                            )as ClassName 
                            ,Name ,Color ,Depiction ,
                            (	
	                            select Name
	                            from Manufacturers as M
	                            where M.ID = p.ManufacturerID
                            )as ManufacturerName
                            ,Price ,InvetoryQuantity
                            from Products as p";
                var r1 = connection.Query<SimpleProductDTO>(sql);
                var r2 = connection.Query<ProductImgDTO>("select ProductID ,Img from Imgs").GroupBy(x => x.ProductID);

                var config = new MapperConfiguration(cfg =>
                                    {
                                        cfg.CreateMap<SimpleProductDTO, SimpleProductViewModel>()
                                            //.ForMember(x => x.Color ,y => y.MapFrom(src => JsonConvert.DeserializeObject<IEnumerable<ColorViewModel>>(src.Color)))
                                            //.ForMember(x => x.Depiction ,y => y.MapFrom(src => JsonConvert.DeserializeObject<DesprictionViewModel>(src.Depiction)))
                                            .ForMember(x => x.ImgList, y => y.Ignore());

                                        cfg.CreateMap<IGrouping<string, ProductImgDTO>, SimpleProductViewModel>()
                                            .ForMember(dest => dest.ID, y => y.MapFrom(src => src.Key))
                                            .ForMember(dest => dest.ImgList, y => y.MapFrom(src => src.Select(x => x.Img)))
                                            .ForAllOtherMembers(x => x.Ignore());
                                    }
                                );
                var mapper = config.CreateMapper();


                mapper.Map(r1, result);
                foreach (var item in result.Zip(r2, (x, y) => new { x, y }))
                {
                    mapper.Map(item.y, item.x);
                }


            }
            return result;
        }

        public IEnumerable<SimpleProductViewModel> GetNewestSimpleProduct(int n)
        {
            IEnumerable<SimpleProductViewModel> result = null;
            using (var connection = new SqlConnection(_conn))
            {
                var sql = @"    select top (@qnum)
                                p.ID ,
                                (	
	                                select Name
	                                from Class as cl
	                                where cl.ID = p.ClassID
                                )as ClassName 
                                ,Name ,Color ,p.Depiction ,
                                (	
	                                select Name
	                                from Manufacturers as M
	                                where M.ID = p.ManufacturerID
                                )as ManufacturerName
                                ,Price ,InvetoryQuantity ,
                                (
	                                select top 1 
	                                in_pc.PurchasingDay
	                                from Procurement as in_pc
	                                where in_pc.ProductID = p.ID
	                                order by CASE WHEN in_pc.PurchasingDay IS NULL THEN 1 ELSE 0 END, in_pc.PurchasingDay desc
                                )as PurchasingDay
                                from Products as p
                                left outer join [Order Details] as od on p.ID = od.ProductID
                                left outer join Orders as o on od.OrderID = o.ID
                                left outer join Procurement as pc on p.ID = pc.ProductID
                                order by CASE WHEN pc.PurchasingDay IS NULL THEN 1 ELSE 0 END, pc.PurchasingDay desc
                            ";
                var sqlImg = "select ProductID ,Img from Imgs where ProductID in @PIDS";
                var qr1 = connection.Query<NewestProductDTO>(sql, new { qnum = n });
                var qr2 = connection.Query<ProductImgDTO>(sqlImg, new { PIDS = qr1.Select(x => x.ID).ToArray() });

                result = qr1.Select(x => new SimpleProductViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    Price = x.Price,
                    ImgList = qr2.Where(y => y.ProductID == x.ID).Select(y => y.Img)
                });
            }

            return result;
        }
        public IEnumerable<SimpleProductViewModel> GetPopularSimpleProduct(int n)
        {
            IEnumerable<SimpleProductViewModel> result = null;
            using (var connection = new SqlConnection(_conn))
            {
                var sql = @"
                            select top (@qnum)
                            p.ID ,
                            (	
	                            select Name
	                            from Class as cl
	                            where cl.ID = p.ClassID
                            )as ClassName 
                            ,Name ,Color ,p.Depiction ,
                            (	
	                            select Name
	                            from Manufacturers as M
	                            where M.ID = p.ManufacturerID
                            )as ManufacturerName
                            ,Price ,InvetoryQuantity 
                            ,(
	                            select count(*)
	                            from [Order Details] as od
	                            where od.ProductID = p.ID
	                            group by od.ProductID
                            ) as amount
                            from Products as p
                            left outer join [Order Details] as od on p.ID = od.ProductID
                            order by amount
                            ";
                var sqlImg = "select ProductID ,Img from Imgs where ProductID in @PIDS";
                var qr1 = connection.Query<PopularSimpleProductDTO>(sql, new { qnum = n });
                var qr2 = connection.Query<ProductImgDTO>(sqlImg, new { PIDS = qr1.Select(x => x.ID).ToArray() });

                result = qr1.Select(x => new SimpleProductViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    Price = x.Price,

                    ImgList = qr2.Where(y => y.ProductID == x.ID).Select(y => y.Img)
                });
            }

            return result;
        }
        public IEnumerable<SortableProductViewModel> GetSortableProductByCatagory(string caID, string clID)
        {
            IEnumerable<SortableProductViewModel> result = null;
            using (var connection = new SqlConnection(_conn))
            {
                var sql = @"
                                select
                                p.ID ,p.ClassID ,ca.ID as CatagoryID
                                ,p.Name ,Color ,p.Depiction ,
                                (	
	                                select Name
	                                from Manufacturers as M
	                                where M.ID = p.ManufacturerID
                                )as ManufacturerName
                                ,Price ,InvetoryQuantity ,
                                (
	                                select top 1 
	                                in_pc.PurchasingDay
	                                from Procurement as in_pc
	                                where in_pc.ProductID = p.ID
	                                order by CASE WHEN in_pc.PurchasingDay IS NULL THEN 1 ELSE 0 END, in_pc.PurchasingDay desc
                                )as PurchasingDay,
                                ISNULL((
	                                select SUM(in_od.Quantity)
	                                from [Order Details] as in_od
	                                where in_od.ProductID = p.ID
	                                group by in_od.ProductID
                                ),0) as 'OrderAmount'
                                from Products as p
                                join Class as cl on p.ClassID = cl.ID
                                join Catagory as ca on ca.ID = cl.CatagoryID
                            ";
                var sqlImg = "select ProductID ,Img from Imgs where ProductID in @PIDS";
                var qr1 = connection.Query<SortableProductViewModel>(sql);
                var qr2 = connection.Query<ProductImgDTO>(sqlImg, new { PIDS = qr1.Select(x => x.ID).ToArray() });

                result = qr1.Select(x => new SortableProductViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    Price = x.Price,
                    ClassID = x.ClassID,
                    CatagoryID = x.CatagoryID,
                    OrderAmount = x.OrderAmount,
                    PurchasingDay = x.PurchasingDay,
                    InvetoryQuantity = x.InvetoryQuantity,
                    ImgList = qr2.Where(y => y.ProductID == x.ID).Select(y => y.Img)
                });
            }

            if (clID != null)
            {
                result = result.Where(x => x.ClassID.Trim() == clID.Trim());
            }
            else if (caID != null)
            {
                result = result.Where(x => x.CatagoryID.Trim() == caID.Trim());
            }
            return result;
        }

        public IEnumerable<ProductViewModel> GetAllProduct()
        {
            IEnumerable<ProductViewModel> result = null;

            using (var connection = new SqlConnection(_conn))
            {
                var sql = @"
                            select p.ID ,p.Name ,
                            (	
	                            select Name
	                            from Class as in_cl
	                            where in_cl.ID = p.ClassID
                            )as ClassName ,
                            (	
	                            select Name
	                            from Catagory as in_ca
	                            where in_ca.ID = cl.CatagoryID
                            )as CatagoryName ,
                            (	
	                            select Name
	                            from Manufacturers as M
	                            where M.ID = p.ManufacturerID
                            )as ManufacturerName
                            ,Price ,InvetoryQuantity  ,

                            p.Color ,p.Depiction ,img.Img ,rp.RelationPID ,pa.Name paName ,pa.Value paValue
                            from Products as p
                            left outer join Manufacturers as man on p.ManufacturerID = man.ID
                            join Class as cl on p.ClassID = cl.ID
                            join Catagory as ca on cl.CatagoryID = ca.ID
                            join Imgs as img on p.ID = img.ProductID
                            left outer join RelatedProducts as rp on p.ID = rp.ProductID
                            left outer join [Product Attributes] as pa on p.ID = pa.PID
                           ";
                var queryResult = connection.Query<ProductDTO>(sql);

                result = queryResult.GroupBy(x => x.ID).Select(x =>
                {
                    var tempD = JsonConvert.DeserializeObject<DescriptionViewModel>(x.FirstOrDefault().Depiction);
                    var unSortAttr = tempD.AttrList?.Select(y => new PAttributeViewModel()
                    {
                        Name = y.Split(':')[0],
                        value = y.Split(':')[1]
                    });
                    var sortAttr = x.GroupBy(y => y.RelationPID).Select(y => y.Key);
                    sortAttr = (sortAttr.All(y => y == null) ? null : sortAttr);

                    return new ProductViewModel()
                    {
                        ID = x.Key,
                        Name = x.FirstOrDefault().Name,
                        ClassName = x.FirstOrDefault().ClassName,
                        CatagoryName = x.FirstOrDefault().CatagoryName,
                        Color = JsonConvert.DeserializeObject<List<ColorViewModel>>(x.FirstOrDefault().Color),
                        Depiction = tempD.Desc,
                        ImgList = x.Select(y => y.Img).Distinct(),
                        InvetoryQuantity = x.FirstOrDefault().InvetoryQuantity,
                        ManufacturerName = x.FirstOrDefault().ManufacturerName,
                        Price = x.FirstOrDefault().Price,
                        RelatedProductList = sortAttr,
                        PAttributeList = x.GroupBy(y =>  y.paName).Select(y => new PAttributeViewModel()
                        {
                            Name = y.Key,
                            value = y.FirstOrDefault().paValue
                        }),
                        UnSortAttributeList = unSortAttr
                    };
                });
                //result = r1.Select(x =>
                //{
                //    x.ImgList = _context.Imgs.Where(y => y.ProductID == x.ID).Select(y => y.Img);
                //    x.PAttributeList = _context.Product_Attributes.Where(y => y.PID == x.ID).Select(y => new PAttributeViewModel()
                //    {
                //        Name = y.Name,
                //        value = y.Value
                //    });
                //    x.RelatedProductList = _context.RelatedProducts.Where(y => y.ProductID == x.ID).Select(y => new RelatedProductViewModel()
                //    {
                //        PID = y.RelationPID
                //    });
                //    return x;
                //});
            }
            return result;
        }
        public ProductViewModel GetProductDetail(string ID)
        {
            IEnumerable<ProductViewModel> result = null;
            using (var connection = new SqlConnection(_conn))
            {
                var sql = @"
	                    select p.ID ,p.Name ,
                        (	
	                        select Name
	                        from Class as in_cl
	                        where in_cl.ID = p.ClassID
                        )as ClassName ,
                        (	
	                        select Name
	                        from Catagory as in_ca
	                        where in_ca.ID = cl.CatagoryID
                        )as CatagoryName ,
                        (	
	                        select Name
	                        from Manufacturers as M
	                        where M.ID = p.ManufacturerID
                        )as ManufacturerName
                        ,Price ,InvetoryQuantity  ,

                        p.Color ,p.Depiction
                        from Products as p
                        left outer join Manufacturers as man on p.ManufacturerID = man.ID
                        join Class as cl on p.ClassID = cl.ID
                        join Catagory as ca on cl.CatagoryID = ca.ID
                        where p.ID = @id
                           ";
                var r1 = connection.Query<ProductDTO>(sql, new { id = ID });
                var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<ProductDTO, ProductViewModel>()
                            .ForMember(x => x.Color, y => y.Ignore())
                );
                var mapper = config.CreateMapper();
                var rvm = mapper.Map<List<ProductViewModel>>(r1);

                result = rvm.Zip(r1, (x, b) =>
                {
                    x.ImgList = _context.Imgs.Where(y => y.ProductID == x.ID).Select(y => y.Img);
                    x.PAttributeList = _context.Product_Attributes.Where(y => y.PID == x.ID).Select(y => new PAttributeViewModel()
                    {
                        Name = y.Name,
                        value = y.Value
                    });

                    var desResult = JsonConvert.DeserializeObject<DescriptionViewModel>(x.Depiction);
                    if (desResult != null)
                    {
                        x.Depiction = desResult.Desc;
                        var anotherAttr = desResult.AttrList?.Select(y => new PAttributeViewModel()
                        {
                            Name = y.Split(':')[0],
                            value = y.Split(':')[1]
                        }).ToList();
                        if (anotherAttr != null)
                        {
                            x.PAttributeList = anotherAttr.Concat(x.PAttributeList);
                        }
                    }


                    x.RelatedProductList = _context.RelatedProducts.Where(y => y.ProductID == x.ID).Select(y => y.RelationPID);
                    x.Color = JsonConvert.DeserializeObject<List<ColorViewModel>>(b.Color);
                    return x;
                });
            }
            return result.FirstOrDefault();
        }

        public IEnumerable<CatagoryViewModel> GetAllCatagory()
        {
            var result = (from ca in _context.Catagory
                          join cl in _context.Class on ca.ID equals cl.CatagoryID
                          select new { ca, cl }).GroupBy(x => x.ca.ID).Select(x => new CatagoryViewModel()
                          {
                              ID = x.Key,
                              Name = x.FirstOrDefault().ca.Name,
                              SubCatagoryList = x.Select(y => new ClassViewModel()
                              {
                                  ID = y.cl.ID,
                                  Name = y.cl.Name
                              })
                          });
            return new List<CatagoryViewModel>()
            {
                 new CatagoryViewModel()
                 {
                     ID = null,
                     Name = "All",
                     SubCatagoryList = null
                 }
            }.Concat(result);
        }
    }
}