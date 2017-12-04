using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SafetyFileHosting.Startup))]
namespace SafetyFileHosting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
