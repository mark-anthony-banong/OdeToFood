using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OdeToFoodFinal.Startup))]
namespace OdeToFoodFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
