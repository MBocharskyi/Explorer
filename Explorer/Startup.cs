using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Explorer.Startup))]

namespace Explorer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
