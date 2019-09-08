using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(qMIne.Startup))]
namespace qMIne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
