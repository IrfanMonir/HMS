using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HMSNew.Startup))]
namespace HMSNew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
