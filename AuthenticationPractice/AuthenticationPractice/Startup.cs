using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthenticationPractice.Startup))]
namespace AuthenticationPractice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
