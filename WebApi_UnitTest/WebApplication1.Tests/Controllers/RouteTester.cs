using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Hosting;
using Xunit;

namespace WebApplication1.Models
{
    public static class RouteTester
    {
        public static void TestRoutes(HttpConfiguration configuration,HttpRequestMessage request,Action<Type,string> callback)
        //public RouteTester(HttpConfiguration configuration, HttpRequestMessage request, Action<Type, string> callback)

        {
            var routeData = configuration.Routes.GetRouteData(request);
            //request.Properties[HttpPropertyKeys.HttpConfigurationKey] = configuration;
            request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;

            var controllerSelector = new DefaultHttpControllerSelector(configuration);
            var controllerContext = new HttpControllerContext(configuration, routeData, request);
            controllerContext.ControllerDescriptor = controllerSelector.SelectController(request);
            var actionSelector = new ApiControllerActionSelector();
            var action = actionSelector.SelectAction(controllerContext).ActionName;
            var controllerType = controllerContext.ControllerDescriptor.ControllerType;

            callback(controllerType, action);//利用该回调函数进行断言，实现单元测试

        }
    }
}