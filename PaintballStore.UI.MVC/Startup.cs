using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaintballStore.UI.MVC.Startup))]
namespace PaintballStore.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
