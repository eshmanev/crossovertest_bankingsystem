using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankingSystem.Portal.Startup))]
namespace BankingSystem.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
