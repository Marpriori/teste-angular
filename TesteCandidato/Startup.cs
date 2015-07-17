using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TesteCandidato.Startup))]
namespace TesteCandidato
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
