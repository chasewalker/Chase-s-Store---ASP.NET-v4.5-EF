using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoreWeb.Startup))]
namespace StoreWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
