using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KEC.Curation.PublishersUI.Startup))]
namespace KEC.Curation.PublishersUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
