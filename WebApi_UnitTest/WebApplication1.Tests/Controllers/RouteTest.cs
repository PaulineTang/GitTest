using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Tests.Controllers
{
    /// <summary>
    /// RouteTest 的摘要说明
    /// </summary>
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class RouteTest
    {

        //**************对路由进行单元测试******************
        //Web Api 没有直接提供 对路由进行单元测试的任何支持——so, we need 定制代码
        //定制代码通常借助一些组件，ex:DefaultHttpControllerSelector、DefaultControllerActionSelector
        //从而实现为给定的HttpRequestMessage、路由配置 推导出控制器和操作名
        //下面是测试路由的泛型方法：借助RouteTester类
        [TestMethod]
        //
        //[ExpectedException(typeof(ArgumentNullException))]//Xunit.NET取消了该特性
        //// Initializes a new instance of the Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedExceptionAttribute
        //// class with an expected exception.
        public void tyzRouteTest()//vs2013单元测试中所测试的方法必须是public的，否则无法进行单元测试。
        {
            //配置需要测试的路由
            var configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute("tyzApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            //配置请求
            var request = new HttpRequestMessage(HttpMethod.Get, "http://tyx.com/api/My/2");

            //根据配置和请求的实例，用RouteTester类调用控制器类型和操作名，验证路由配置
            RouteTester.TestRoutes(configuration, request,
                (controllerType, action) =>
                {
                    Xunit.Assert.Equal(typeof(MyController), controllerType);
                    Xunit.Assert.Equal("Get", action);
                }
            );




            //StringAssert.
            //Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotSame

        }
        //



    }
}
