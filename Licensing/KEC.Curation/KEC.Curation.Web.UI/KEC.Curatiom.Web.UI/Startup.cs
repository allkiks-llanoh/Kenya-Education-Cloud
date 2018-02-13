using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KEC.Curatiom.Web.UI.Startup))]
namespace KEC.Curatiom.Web.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
