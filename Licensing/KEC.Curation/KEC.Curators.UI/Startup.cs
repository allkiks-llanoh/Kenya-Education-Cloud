using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KEC.Curators.UI.Startup))]
namespace KEC.Curators.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
