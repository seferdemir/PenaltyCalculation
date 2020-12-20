using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PenaltyCalculation.Startup))]
namespace PenaltyCalculation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
