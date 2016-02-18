using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gilmond.Website.Startup))]
namespace Gilmond.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
