using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
//开发人员会修改这个文件以满足某个应用程序的需求
namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();
            //web api的主要路由注册是MapHttpRoute扩展方法
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );//该文件默认包含了一个路由示例
        }
    }
}
