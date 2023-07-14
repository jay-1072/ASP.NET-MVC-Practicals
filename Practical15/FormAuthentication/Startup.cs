using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FormAuthentication.Startup))]
namespace FormAuthentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
