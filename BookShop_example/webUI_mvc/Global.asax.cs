using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using webUI_mvc.Infrastructure;

namespace webUI_mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //设置Controller工厂,设置自定义的Controller工厂必须要在这注册
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }
    }
}
