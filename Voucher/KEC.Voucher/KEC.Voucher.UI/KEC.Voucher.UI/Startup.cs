using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KEC.Voucher.UI.Startup))]
namespace KEC.Voucher.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
