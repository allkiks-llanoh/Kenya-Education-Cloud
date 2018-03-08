using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KEC.Curation.UI.Startup))]
namespace KEC.Curation.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
