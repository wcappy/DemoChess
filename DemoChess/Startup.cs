using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemoChess.Startup))]
namespace DemoChess
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
