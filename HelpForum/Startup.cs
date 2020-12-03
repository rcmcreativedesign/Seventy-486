using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelpForum.Startup))]
namespace HelpForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
