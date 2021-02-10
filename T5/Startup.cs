using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(T5.Startup))]
namespace T5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
