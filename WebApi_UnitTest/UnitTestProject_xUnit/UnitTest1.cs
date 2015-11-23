using Moq;
using WebApplication1.IServices;
using WebApplication1.Controllers;
using Xunit;
using WebApplication1.Models;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject_xUnit
{
    public class UnitTest1
    {
        private Mock<IssuesSource> _mockIssuesSource = new Mock<IssuesSource>();
        private MyController _myController;
        public UnitTest1()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
            _myController = new MyController(_mockIssuesSource.Object);
        }

        //[Fact]
        [TestMethod]
        public void POSTtwoTest()
        {
            var badissue = new Issue();//测试应该通不过
            var createdIssue = new Issue();
            createdIssue.Id = "1";
            _mockIssuesSource.Setup(i => i.CreatAsync(createdIssue)).Returns(() => Task.FromResult(badissue));
            //操作
            var result = _myController.Post(createdIssue, false).Result as CreatedAtRouteNegotiatedContentResult<Issue>;

            //_mockIssuesSource.Setup(service => service.ServiceName).Returns("TYZservice");//设置属性值
            
            //断言
            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsType<Issue>(result.Content);
        }
    }
}
