using BloodTypess.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace BloodTypess.Web.Seeders
{
	public class RoleSeeder
	{
		public static async Task CreateRolesAsync(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

			string[] roleNames = { "Admin", "Employee", "User" };

			foreach (var roleName in roleNames)
			{
				var roleExists = await roleManager.RoleExistsAsync(roleName);

				if (!roleExists)
				{
					await roleManager.CreateAsync(new IdentityRole(roleName));
				}
			}

			// Create an admin user if none exists
			var adminEmail = "admin@blood.com";  
			var adminUser = await userManager.FindByEmailAsync(adminEmail);

			if (adminUser == null)
			{
				var NewUser = new AppUser
				{
					UserName= adminEmail,
					Email = adminEmail,
					EmailConfirmed = true,
					FirstName = "Admin",
					LastName = "User"
 				};

				var result = await userManager.CreateAsync(NewUser, "Admin@123");  

				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(NewUser, "Admin");
				}
			}
		}
	}
}
