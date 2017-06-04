using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AngularTreeView.Startup))]
namespace AngularTreeView
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
