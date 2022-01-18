using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Academy_Portal.Startup))]
namespace Academy_Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
