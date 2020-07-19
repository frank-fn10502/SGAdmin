using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SurvivalGameAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "NewestSimpleProduct",
                routeTemplate: "api/simpleProduct/Newest/{n}",
                defaults: new { controller = "Product", action = "NewestSimpleProduct", n = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "MostPopularSimpleProduct",
                routeTemplate: "api/simpleProduct/Newest/{n}",
                defaults: new { controller = "Product", action = "MostPopularPopularSimpleProduct", n = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ProductSortByCatagory",
                routeTemplate: "api/sortableProduct/{caID}/{clID}",
                defaults: new { controller = "Product", action = "SortableProductByCatagory", caID = RouteParameter.Optional, clID = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "ProductDetail",
                routeTemplate: "api/detail/{id}",
                defaults: new { controller = "Product", action = "ProductDetail", id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Catagory",
                routeTemplate: "api/catagory",
                defaults: new { controller = "Product", action = "AllCatagory" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
