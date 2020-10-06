using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OneSignalAPPMngr.Models;
using Owin;
using System.Security.Claims;
using System.Net.Http;
//using System.Web.Http.SelfHost;

[assembly: OwinStartupAttribute(typeof(OneSignalAPPMngr.Startup))]
namespace OneSignalAPPMngr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
			
			ConfigureAuth(app);
			createRolesandUsers();
		}


		private void createRolesandUsers()
		{
			ApplicationDbContext context = new ApplicationDbContext();

			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

			// In Startup iam creating first Admin Role and creating a default Admin User 
			if (!roleManager.RoleExists("Admin"))
			{

				// first we create Admin rool
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Admin";
				roleManager.Create(role);

				//Here we create a Admin super user who will maintain the website				

				var user = new ApplicationUser();
				user.UserName = "admin123";
				user.Email = "admin@gmail.com";

				string userPWD = "A@Z200711";

				var chkUser = UserManager.Create(user, userPWD);

				//Add default User to Role Admin
				if (chkUser.Succeeded)
				{
					var result1 = UserManager.AddToRole(user.Id, "Admin");

				}
			}

			
			if (!roleManager.RoleExists("Data Entry"))
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "DataEntry";
				roleManager.Create(role);

			}
		}
	}
}
