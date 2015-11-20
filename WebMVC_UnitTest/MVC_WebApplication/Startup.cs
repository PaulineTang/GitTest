using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_WebApplication.Startup))]
namespace MVC_WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
