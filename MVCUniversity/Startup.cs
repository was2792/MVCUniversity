using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCUniversity.Startup))]
namespace MVCUniversity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
