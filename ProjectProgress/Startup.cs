using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ProjectProgress.Models;

[assembly: OwinStartupAttribute(typeof(ProjectProgress.Startup))]
namespace ProjectProgress
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserRole();
        }

        public void CreateUserRole()
        {
            var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            if (!roleManger.RoleExists("User"))
            {
                var role = new IdentityRole("User");
                roleManger.Create(role);
            }
        }
  
    }
}
