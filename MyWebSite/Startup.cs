using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using MyWebSite.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyWebSite.Startup))]
namespace MyWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new IdentityRole { Name = "Admin" };
                    roleManager.Create(role);

                    var user = new ApplicationUser
                    {
                        UserName = "admin@shop.local",
                        Email = "admin@shop.local"
                    };

                    var userPwd = "adminPwd";
                    var createResult = userManager.Create(user, userPwd);

                    if (createResult.Succeeded)
                    {
                        var result = userManager.AddToRole(user.Id, "Admin");
                    }
                }
            }
        }
    }
}
