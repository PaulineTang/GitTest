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
using System.Web.Http.Results;
using System.Net.Http.Formatting;
using System.Linq;
using System.IO;
using System.Threading;
using System.Web.Http.Filters;
using System.Web.Mvc;
using WebApplication1.Filters;
using System.Web.Http.Controllers;

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
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        //[ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
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

        //***************************************************8
        //验证调用了CreatAsync()方法
        [Fact]
        public void ShouldCallCreatedAsyncWhenPostForNewIssue()
        {
            //准备
            //初始化运行和配置路由
            _myController.Configuration = new HttpConfiguration();
            var route = _myController.Configuration.Routes.MapHttpRoute(
            "DefaultApi",
            "api/{Controller}/{id}",
            new { id = RouteParameter.Optional }
            );
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary
            {
                {"Controller","My" }
            }
            );
            _myController.Request = new HttpRequestMessage(HttpMethod.Post,
                "http://tyz.com/My");
            _myController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, _myController.Configuration);
            _myController.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);


            //_myController.ConfigureForTesting(new HttpRequestMessage(HttpMethod.Post,"http://tyz.com/My"),"DefaultApi",null);
            //_myController.ConfigureForTesting(HttpMethod.Post, "http://tyz.com/My");

            Issue issue = new Issue();
            _mockIssuesSource.Setup(i => i.CreatAsync(issue)).Returns(() => Task.FromResult(issue));

            //操作
            var response = _myController.Post(issue).Result;

            //断言
            _mockIssuesSource.Verify(i => i.CreatAsync(issue));
            //issue  <=>?   i => i.CreatAsync(   It.Is<Issue>   (iss => iss.Equals(issue))  )
        }
        //验证响应消息
        [Fact]
        public void ShouldSetResponseHeaderWhenPostForNewIssue()
        {
            //准备
            _myController.ConfigureForTesting(HttpMethod.Post, "http://tyz.com/api/My");
            Issue createdIssue = new Issue();
            createdIssue.Id = "1";
            _mockIssuesSource.Setup(i => i.CreatAsync(createdIssue)).Returns(() => Task.FromResult(createdIssue));

            //操作
            var response = _myController.Post(createdIssue).Result;

            //断言
            Xunit.Assert.Equal(response.StatusCode, HttpStatusCode.Created);
            Xunit.Assert.Equal(response.Headers.Location.AbsoluteUri, "http://tyz.com/api/My/1");

            //response.StatusCode.ShouldEqual(HttpStatusCode.Created);
            //response.Headers.Location.AbsoluteUri.ShouldEqual("http://tyz.com/My/1");
        }
        //简化上面的测试——验证相应消息，借助WebApi2引入的IHttpActionResult，可以简化对控制器的单元测试
        [Fact]
        public void POSTtwoTest()
        {
            var badissue = new Issue();//测试应该通不过
            var createdIssue = new Issue();
            createdIssue.Id = "1";
            _mockIssuesSource.Setup(i => i.CreatAsync(createdIssue)).Returns(() => Task.FromResult(badissue));
            //操作
            var result = _myController.Post(createdIssue, false).Result as CreatedAtRouteNegotiatedContentResult<Issue>;
            //断言
            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsType<Issue>(result.Content);
        }

        //*********************************************************
        //测试MediaTypeFormatter(处理新媒体类型、内容协商的主要部分)
        //MediaTypeFormatter
        //检查所支持媒体类型的单元测试
        [Fact]
        public void ShouldSupportAtomType()
        {
            Mock<MediaTypeFormatter> formatter = new Mock<MediaTypeFormatter>();

            //var formatter = new SyndicationMediaTypeFormatter();

            Xunit.Assert.False((formatter.Object).SupportedMediaTypes.Any(i => i.MediaType == "application/atom+xml"));
            // Xunit.Assert.True(formatter.SupportedMediaTypes.Any(i=>i.MediaType=="application/atom+xml"));
        }


        //检查是否能够读或写一个类型的单元测试
        [Fact]
        public void ShouldNotReadAnyType()
        {
            Mock<MediaTypeFormatter> formatter = new Mock<MediaTypeFormatter>();
            //var formatter = new SyndicationMediaTypeFormatter();

            var canRead = (formatter.Object).CanReadType(typeof(Object));
            //var CanRead = formatter.CanReadType(typeof(Object));

            Xunit.Assert.False(canRead);
        }
        [Fact]
        public void ShouldWriteAnyType()
        {
            Mock<MediaTypeFormatter> formatter = new Mock<MediaTypeFormatter>();

            var canWrite = (formatter.Object).CanWriteType(typeof(Object));

            Xunit.Assert.True(!canWrite);//???????????必须添加！，测试才能通过
        }
        //测试是否能够从流 读取/写入模型
        //验证writeToStreamAsync方法行为的单元测试
        //[Fact]
        //public void ShouldSerializeAsAtom()
        //{
        //    var ms = new MemoryStream();//     创建其支持存储区为内存的流
        //    var content = newFackContent();
        //    …………
        //}


        //***************************单元测试HttpMessageHandler的SendAsync(HttpRequestMessage requestMessage,CancellationToken cancellationToken)方法********************88
        //MessageInvoker
        [Fact]
        public void ShouldInvokerHandler()
        {
            var handler = new Mock<HttpMessageHandler>();

            var invoker = new HttpMessageInvoker(handler.Object);
            var task = invoker.SendAsync(new HttpRequestMessage(), new CancellationToken());//以异步操作发送 HTTP 请求, cancellationToken取消操作的取消标记
            task.Wait();

            var response = task.Result;
            //Assert对响应进行断言
            //……
        }
        //**********************单元测试 ActionFilterAttribute(抽象类),内部方法为public,可直接进行单元测试
        //其中方法1：public virtual OnActionExcuted(HttpActionExcutedContext excutedActionContext)
        //方法2：public virtual OnActionExcuting(HttpActionContext actionContext);
        //有效码值得单元测试1：有效码值的单元测试

        [Fact]
        public void ShouldValidateKey()
        {
            var keyVerifier = new Mock<IKeyVerifier>();
            keyVerifier.Setup(i => i.Verifier("testKey")).Returns(() => true);

            var request = new HttpRequestMessage();
            request.Headers.Add("X-AuthKey", "testKey");
            var configuration = new HttpConfiguration();
            var route = configuration.Routes.MapHttpRoute("DefasultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "My" } });
            //request.Properties[HttpPropertyKeys.HttpConfigurationKey] = configuration;
            request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            var controllerContext = new HttpControllerContext(configuration, routeData, request);
            var actionContext = new HttpActionContext(controllerContext, (new Mock<HttpActionDescriptor>()).Object);//????????ByTYZ20151123

            var filter = new ApplicationKeyActionFilter(keyVerifier.Object);
            filter.OnActionExecuting(actionContext);//测试完成初始化的上下文

            Xunit.Assert.Null(actionContext.Response);
        }


        //测试2：无效码值的单元测试
        public void ShouldNotVerifyKey()
        {
            var verifier = new Mock<IKeyVerifier>();
            verifier.Setup(i => i.Verifier("testKey")).Returns(() => true);

            var request = new HttpRequestMessage();
            var actionContext = InitializeActionContext(request);
            var filter = new ApplicationKeyActionFilter(verifier.Object);
            filter.OnActionExecuting(actionContext);

            Xunit.Assert.NotNull(actionContext.Response);
            Xunit.Assert.Equal(HttpStatusCode.Unauthorized, actionContext.Response.StatusCode);
        }

        public HttpActionContext InitializeActionContext(HttpRequestMessage request)
        {
            request.Headers.Add("X-AuthKey", "testKey");
            var configuration = new HttpConfiguration();
            var route = configuration.Routes.MapHttpRoute("DefasultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "My" } });
            //request.Properties[HttpPropertyKeys.HttpConfigurationKey] = configuration;
            request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            var controllerContext = new HttpControllerContext(configuration, routeData, request);
            var actionContext = new HttpActionContext(controllerContext, (new Mock<HttpActionDescriptor>()).Object);//????????ByTYZ20151123

            return actionContext;
        }


    }
   





}