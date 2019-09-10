using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(qMine.Startup))]
namespace qMine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
