using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PeekApp.Startup))]
namespace PeekApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
