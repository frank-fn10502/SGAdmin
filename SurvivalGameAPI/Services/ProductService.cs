using AutoMapper;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using SurvivalGameAPI.CustomModels.DTOModel;
using SurvivalGameAPI.CustomModels.ViewModel;
using SurvivalGameAPI.CustomModels.ViewModel.Product;
using SurvivalGameAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.Services
{
    public class ProductService
    {
        private ProductRepository _pRepository;
        public ProductService()
        {
            _pRepository = new ProductRepository();
        }
        //public IEnumerable<SimpleProductViewModel> GetAllProduct()
        //{
        //    var queryResult = _pRepository.GetAllSimpleProduct();
        //    var result = queryResult;
        //    //var config = new MapperConfiguration(cfg =>
        //    //    cfg.CreateMap<ProductDTO ,ProductViewModel>()
        //    //    .ForMember(x => x.Color ,y => y.MapFrom(src => JsonConvert.DeserializeObject<IEnumerable<ColorViewModel>>(src.Color)))
        //    //    .ForMember(x => x.Depiction ,y => y.MapFrom(src => JsonConvert.DeserializeObject<DesprictionViewModel>(src.Depiction)))
        //    //);
        //    //var mapper = config.CreateMapper();
        //    //var pvm = mapper.Map<IEnumerable<ProductViewModel>>(queryResult).GroupBy(x => x.ID);

        //    //var groupQueryResult = queryResult.GroupBy(x => x.ID).ToList();
        //    //IEnumerable<ProductViewModel> result = pvm.Zip(groupQueryResult ,(x ,y) =>
        //    //{
        //    //    x.FirstOrDefault().ImgList = y.Select(z => z.Img);
        //    //    return x.FirstOrDefault();
        //    //});
        //    //var config = new MapperConfiguration(cfg =>
        //    //    cfg.CreateMap<IGrouping<string ,ProductDTO> ,ProductViewModel>()
        //    //    .ForMember(x => x.Color ,y => y.MapFrom(src => JsonConvert.DeserializeObject<IEnumerable<ColorViewModel>>(src.FirstOrDefault().Color)))
        //    //    .ForMember(x => x.Depiction ,y => y.MapFrom(src => JsonConvert.DeserializeObject<DesprictionViewModel>(src.FirstOrDefault().Depiction)))
        //    //    .ForMember(x => x.ImgList ,y => y.MapFrom(src => src.Select(x => x.Img)))
        //    //    .ForAllOtherMembers(x => x.Ignore())

        //    //);
        //    //var mapper = config.CreateMapper();
        //    //var pvm = mapper.Map<IEnumerable<ProductViewModel>>(queryResult);
        //    return result;
        //}
        public ProductTableViewModel GetProductTable()
        {
            var ptvm = new ProductTableViewModel()
            {
                FiledList = new List<string>()
                 {
                     "ID" ,"Name" ,"ClassName","ManufacturerName" ,"Color","InvetoryQuantity" ,"Price"
                 },
                SvmList = GetAllProduct()
            };
            return ptvm;
        }

        public IEnumerable<SimpleProductViewModel> GetNewestSimpleProduct(int n)
        {
            return _pRepository.GetNewestSimpleProduct(n);
        }
        public IEnumerable<SimpleProductViewModel> GetPopularSimpleProduct(int n)
        {
            return _pRepository.GetPopularSimpleProduct(n);
        }
        public IEnumerable<SortableProductViewModel> GetSortableProductByCatagory(string caID, string clID)
        {
            return _pRepository.GetSortableProductByCatagory(caID, clID);
        }
        public IEnumerable<ProductViewModel> GetAllProduct()
        {
            return _pRepository.GetAllProduct();
        }
        public ProductViewModel GetProductDetail(string ID)
        {
            return _pRepository.GetProductDetail(ID);
        }
        public IEnumerable<CatagoryViewModel> GetAllCatagory()
        {
            return _pRepository.GetAllCatagory().Select(x =>
            {
                if (x.SubCatagoryList != null)
                {
                    var tempList = new List<ClassViewModel>()
                    {
                        new  ClassViewModel(){  ID = null ,Name = "All"}
                    };

                    tempList.AddRange(x.SubCatagoryList);
                    x.SubCatagoryList = tempList;
                }

                return x;
            });
        }
    }
}