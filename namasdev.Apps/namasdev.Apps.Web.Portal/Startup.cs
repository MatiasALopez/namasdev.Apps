using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(namasdev.Apps.Web.Portal.Startup))]
namespace namasdev.Apps.Web.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureServices();
        }
    }
}
