using AutoMapper;
using Dapper;
using SurvivalGameAPI.CustomModels.ViewModel.Member;
using SurvivalGameAPI.Models;
using SurvivalGameAPI.ResultModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.Repositories
{
    public class MemberRepository
    {
        private string _conn = ConfigurationManager.ConnectionStrings["SGModel"].ConnectionString;
        private SGModel _context = new SGModel();
        public CheckOutViewModel GetCheckOut(string orderID)
        {
            CheckOutViewModel result = null;

            using (var connection = new SqlConnection(_conn))
            {
                var sql = @"
                            select Name ,Address  ,Phone ,Mail as Email ,
                            (
	                            select in_pay.PaymentMethod
	                            from PaymentMethod in_pay
	                            where in_pay.ID = o.PaymentMethodID
                            ) as PaymentMethod ,
                            (
	                            select sum(Quantity * Discount * UnitPrice)
	                            from [Order Details]
	                            where OrderID = @oid
	                            group by OrderID
                            ) as Total
                            from Members m
                            left outer join Orders o on m.ID = o.MemberID
                            where o.ID = @oid
                         ";
                result = connection.Query<CheckOutViewModel>(sql, new { oid = orderID }).FirstOrDefault();
            }
            return result;
        }
        public APIResult CheckLogin(LoginViewModel loginVM)
        {
            var result = _context.Members.FirstOrDefault(x => x.Mail == loginVM.Email && x.Password == loginVM.Password);
            var success = (result != null ? true : false);
            if (success)
            {
                return new APIResult()
                {
                    IsSuccess = success,
                    ExceptionString = null,
                    Data = result.ID
                };
            }
            return new APIResult()
            {
                IsSuccess = false,
                ExceptionString = "no account",
                Data = null
            };
        }
        public APIResult CheckRedister(RegisterViewModel registerVM)
        {
            var apiresult = new APIResult();

            try
            {
                var isDuplicate = _context.Members.Any(x => x.Mail == registerVM.Email);
                if (isDuplicate)
                {
                    apiresult.IsSuccess = false;
                    apiresult.ExceptionString = "mail is duplicate!!!";
                    return apiresult;
                }
                var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<RegisterViewModel, Members>()
                    .ForMember(x => x.Mail, src => src.MapFrom(y => y.Email))
                    .ForMember(x => x.Birthday, src => src.MapFrom(y => y.Birth))

                );
                var mapper = config.CreateMapper();
                var result = mapper.Map<Members>(registerVM);
                result.ID = $"MB{(_context.Members.Count() + 1).ToString().PadLeft(3, '0')}";

                _context.Entry(result).State = System.Data.Entity.EntityState.Added;
                _context.SaveChanges();
                apiresult.IsSuccess = true;
            }
            catch (Exception e)
            {
                apiresult.IsSuccess = false;
                apiresult.ExceptionString = e.Message;
            }
            return apiresult;
        }
        public MemberCenterViewModel GetMemberCenter(string memberID)
        {
            var mem = _context.Members.FirstOrDefault(x => x.ID == memberID);
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Members, MemberCenterViewModel>()
                .ForMember(x => x.HistoryItemList, src => src.Ignore())
                .ForMember(x => x.WishlistItemsList, src => src.Ignore())
            );
            var mapper = config.CreateMapper();
            var result = mapper.Map<MemberCenterViewModel>(mem);

            var orderID = _context.Orders.FirstOrDefault(x => x.MemberID == memberID).ID;

            result.HistoryItemList = _context.Order_Details.Where(x => x.OrderID == orderID)
                  .Select(x => new { Products = _context.Products.FirstOrDefault(y => y.ID == x.ProductID), Imgs = _context.Imgs.FirstOrDefault(y => y.ProductID == x.ProductID) }).Select(x => new HistoryItems()
                  {
                      HistoryImg = x.Imgs.Img,
                      HistoryName = x.Products.Name,
                      HistoryPrice = x.Products.Price,
                      HistoryQuantity = x.Products.InvetoryQuantity
                  });

            result.WishlistItemsList = _context.Wishlist.Where(x => x.MemberID == memberID)
                  .Select(x => new { Products = _context.Products.FirstOrDefault(y => y.ID == x.ProductID), Imgs = _context.Imgs.FirstOrDefault(y => y.ProductID == x.ProductID) }).Select(x => new WishlistItems()
                  {
                      WishlistImg = x.Imgs.Img,
                      WishlistName = x.Products.Name,
                      WishlistPrice = x.Products.Price
                  });

            return result;
        }
    }
}