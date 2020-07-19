using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using SurvivalGameAPI.CustomModels.DTOModel;
using SurvivalGameAPI.ResultModel;
using SurvivalGameAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SurvivalGameAPI.Controllers
{
    public class ProductController :ApiController
    {
        [HttpGet]
        public APIResult NewestSimpleProduct(int n)
        {
            return new APIResult()
            {
                IsSuccess = true ,
                Data = new ProductService().GetNewestSimpleProduct(n)
            };
        }
        [HttpGet]
        public APIResult MostPopularPopularSimpleProduct(int n)
        {
            return new APIResult()
            {
                IsSuccess = true ,
                Data = new ProductService().GetPopularSimpleProduct(n)
            };
        }

        [HttpGet]
        public APIResult SortableProductByCatagory(string caID = null, string clID = null)
        {
            return new APIResult()
            {
                IsSuccess = true ,
                Data = new ProductService().GetSortableProductByCatagory(caID ,clID)
            };
        }
        [HttpGet]
        public APIResult ProductDetail(string id)
        {
            return new APIResult()
            {
                IsSuccess = true ,
                Data = new ProductService().GetProductDetail(id)
            };
        }

        [HttpGet]
        public APIResult AllCatagory()
        {
            return new APIResult()
            {
                IsSuccess = true,
                Data = new ProductService().GetAllCatagory()
            };
        }


        /// <summary>
        /// 棄用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public APIResult GetAllProductTable()
        {
            return new APIResult()
            {
                IsSuccess = true,
                Data = new ProductService().GetProductTable()
            };
        }

        /// <summary>
        /// 未實作
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public APIResult TestImgur()
        {
            var client = new ImgurClient("2f3ed3db83e866d" ,"62a894b5e1dd5ed77cc5b4f10eea25e3ebc3e816"
                                        ,new OAuth2Token("654c446bd2f20543284db4a2d986ea234d81b98b"
                                                        ,"e3100d2d2fc8b14e9131dc1aad5b9a24ca8ee22c"
                                                        ,"bearer"
                                                        ,"132433023"
                                                        ,"frankimg"
                                                        ,315360000));
            var endpoint = new ImageEndpoint(client);
            var endpointAlbum = new AlbumEndpoint(client);
            IImage image;
            bool success = false;
            //取得圖片檔案FileStream
            using (var fs = new FileStream(@"D:\Documents\build_school\project\finalProject1\SurvivalGameVer2\SurvivalGame\Assets\images\Product1.png" ,FileMode.Open))
            {
                image = endpoint.UploadImageStreamAsync(fs).GetAwaiter().GetResult();
                success = endpointAlbum.AddAlbumImagesAsync("ZeSZMYK" ,new List<string>()
                {
                    image.Id
                }).GetAwaiter().GetResult();
            }

            return new APIResult()
            {
                IsSuccess = true ,
                //顯示圖檔位置
                Data = $"Image uploaded.Image Url:  + {image.Link} ,ImgId:{image.Id} ,success: {success}"
            };
        }

        /// <summary>
        /// 未使用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public APIResult AllProduct()
        {
            var temp = new ProductService().GetAllProduct();
            return new APIResult()
            {
                IsSuccess = true,
                Data = temp
            };
        }
    }
}