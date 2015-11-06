using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace ConsoleApplication2_selfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly.Load("WebApi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            HttpSelfHostConfiguration configuration = new HttpSelfHostConfiguration("http://localhost/selfhost/tyz");//指定基地址
            try
            {
                using (HttpSelfHostServer httpServer = new HttpSelfHostServer(configuration))
                {
                    httpServer.Configuration.Routes.MapHttpRoute(
                     name: "DefaultApi",
                     routeTemplate: "api/{controller}/{id}",
                     defaults: new { id = RouteParameter.Optional });

                    httpServer.OpenAsync().Wait();
                    Console.Read();
                }
            }
            catch (Exception e)
            {
            }

            ////web API的SelfHost寄宿方式通过HttpSelfHostServer来完成
            //using (HttpSelfHostServer httpServer = new HttpSelfHostServer(configuration))
            //{
            //    httpServer.Configuration.Routes.MapHttpRoute(
            //     name: "DefaultApi",
            //     routeTemplate: "api/{controller}/{id}",
            //     defaults: new { id = RouteParameter.Optional });

            //    httpServer.OpenAsync();//开启后，服务器开始监听来自网络的调用请求
            //    Console.Read();
            //}
        }
    }
}
