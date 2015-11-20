using System;
using System.Web.Http;
using Moq;
using WebApplication1.IServices;
using WebApplication1.Controllers;
using Xunit;
using WebApplication1.Models;
using System.Threading.Tasks;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Routing;
using System.Net.Http;
using System.Web.Http.Hosting;
using WebApiContrib.Testing;

namespace WebApplication1.Tests.Controllers
{
    /// <summary>
    /// MyControllerTest 的摘要说明
    /// </summary>
    [TestClass]
    public class MyControllerTest
    {
        private Mock<IssuesSource> _mockIssuesSource = new Mock<IssuesSource>();
        private MyController _myController;
        public MyControllerTest()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
            _myController = new MyController(_mockIssuesSource.Object);

            _myController.Configuration = new HttpConfiguration();



            ////初始化运行和配置路由
            //var route = _myController.Configuration.Routes.MapHttpRoute(
            //"DefaultApi",
            //"api/{Controller}/{id}",
            //new { id = RouteParameter.Optional }
            //);
            //var routeData = new HttpRouteData(route, new HttpRouteValueDictionary
            //{
            //    {"Controller","My" }
            //}
            //);
            //_myController.Request = new HttpRequestMessage(HttpMethod.Post,
            //    "http://tyz.com/My");
            //_myController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, _myController.Configuration);
            //_myController.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);



            //_myController.ConfigureForTesting(new HttpRequestMessage(HttpMethod.Post, "http://tyz.com/My"), null, null);
        }

        //private TestContext testContextInstance;

        ///// <summary>
        /////获取或设置测试上下文，该上下文提供
        /////有关当前测试运行及其功能的信息。
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        //#region 附加测试特性
        ////
        //// 编写测试时，可以使用以下附加特性: 
        ////
        //// 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        //// [ClassInitialize()]
        //// public static void MyClassInitialize(TestContext testContext) { }
        ////
        //// 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        //// [ClassCleanup()]
        //// public static void MyClassCleanup() { }
        ////
        //// 在运行每个测试之前，使用 TestInitialize 来运行代码
        //// [TestInitialize()]
        //// public void MyTestInitialize() { }
        ////
        //// 在每个测试运行完之后，使用 TestCleanup 来运行代码
        //// [TestCleanup()]
        //// public void MyTestCleanup() { }
        ////
        //#endregion
        ///// <summary>
        ///// VS自带的单元测试框架
        ///// </summary>
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    //
        //    // TODO:  在此处添加测试逻辑
        //    //
        //}

        /// <summary>
        /// X.Unit框架用于单元测试
        /// </summary>
        [Fact]
        public void ShouldReturnIssueWhenGetUsually()
        {
            var issue = new Issue();

            _mockIssuesSource.Setup(i => i.FindAsync("fdg")).Returns(Task.FromResult(issue));

            var foundIssue = _myController.Get("fdg").Result;

            Xunit.Assert.Equal(issue, foundIssue);
        }

        [Fact]
        public void ShouldReturnNotFoundWhenGetForNonExistIssue()
        {
            var issue = new Issue();

            _mockIssuesSource.Setup(i => i.FindAsync("fdg")).Returns(Task.FromResult((Issue)null));

            var exception = Xunit.Assert.Throws<AggregateException>(() =>
            {
                var task = _myController.Get("fdg");
                var result = task.Result;
            });

            Xunit.Assert.IsType<HttpResponseException>(exception.InnerException);
            Xunit.Assert.Equal(HttpStatusCode.NotFound, ((HttpResponseException)exception.InnerException).Response.StatusCode);
        }



        [Fact]
        void ShouldCallCreatedAsyncWhenPostForNewIssue()
        {
            //准备
            _myController.ConfigureForTesting(new HttpRequestMessage(HttpMethod.Post,"http://tyz.com/My"),"DefaultApi",null);
            //_myController.ConfigureForTesting(HttpMethod.Post, "http://tyz.com/My");

            Issue issue = new Issue();
            _mockIssuesSource.Setup(i => i.CreatAsync(issue)).Returns(() => Task.FromResult(issue));

            //操作
            var response = _myController.Post(issue).Result;

            //断言
            _mockIssuesSource.Verify(i => i.CreatAsync(issue));
            //issue  <=>?   i => i.CreatAsync(   It.Is<Issue>   (iss => iss.Equals(issue))  )
        }

    }
}
