//检查预期异常的单元测试
[TestClass]
public class ValuesControllerTests
{
	ValuesController controller;
	public ValuesControllerTests( ValuesController _controller)
	{
		controller=_controller;
	}
	[TestClass]
	public void GetTest()
	{
		controller.Throws<HttpException>(()=>controller.Get("errorParementor"));	
		
	}
}
//使用模拟类的单元测试-利用Moq框架伪造（模拟）的类
//Follow模仿了数据访问类的行为
public class IssuesControllerTests
{
	private Mock<Iissue> _mockIissue= new Mock<Iissue>();
	private IssuesController _controller;
	public IssuesControllerTests()
	{
		_controller=new IssuesController(_mockIissue_.object);
	}
	
	[Fact]
	public void ShouldCallFindAsyncWhenGetForAllIssues()
	{
		_controller.Get();
		
		_mockIissue.Verify(i=>i.FindAsync());
	}
	
//测试Api的控制器
//需提供请求消息（请求消息的初始化）+环境条件配置
	[Fact]
	public void ShouldReturnIssueWhenGetForExistIssue()
	{
		var issue new Issue();
		_mockIissue.SetUp(i=>i.FindAsync("1")).Returns(Task.FromResult(issue));
		
		var foundIssue=_controller.FindAsync("1").Result;
		
		Assert.Equal(issue,foundIssue);
	}
	[Fact]
	void ShouldReturnNotFoundWhenGetForNonExistIssue()
	{
		_mockIissue.SetUp(i=>i.FindAsync("1000")).Returns(Task.FromResult(noll));
		
		var exception=Assert.Throws<AggregateException>(()=>
		{
			var task=_controller.Get("1000")；
			var result=task.Result;
		}
		);
		
		Assert.IsType<HttpResponseException>(exception.InnerException);
		Assert.Equal(HttpStatusCode.NotFound,((HttpResponseException)exception.InnerException).Response.StatusCode);
	}
	//当被测控制器方法依赖HttpRequestMessage以及UrlHelper时，我们需要进行相应的请求和路由配置
	[Fact]
	void ShouldCallCreatedAsyncWhenPostForNewIssue()
	{
		_controller.Configutation=new HttpConfiguration();
		
		var route=_controller.Configutation.Route.HttpMapRout("TYZapi","api/{contrller}/{id}",new{id=RouteParameter.Optional});	
		
		var routeData=new HttoRouteData(route,new HttpRouteValueDictionary{{"contrller","Issues"}});
		
		_controller.Request=new HttpRequestMessage(HttpMethod.POST,"Http://test.com/issues");
		_controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,_controller.Configutation);
		_controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey,routeData);
		
		var issue=new Issue();
		_mockIissue.SetUp(i=>i.CreateAsync(issue)).Returns(()=>Task.FromResult(issue));
		
		var response=_controller.CreateAsync(issue).Request;
		
		_mockIissue.Verify(i=>i.CreateAsync(issue));//验证调用了CreatAsync		
	}
}

//*******************HttpMessageHandler的定义
public abstract HttpMessageHandler()
{
	protected internal Task<HttpResponseMessage> SendAsync(HttpRequestMessage questMessage,CancellationToken cancellationToken);//非公开方法不能直接在单元测试中使用
}

