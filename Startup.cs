using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DG_BugTracker.Startup))]
namespace DG_BugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
