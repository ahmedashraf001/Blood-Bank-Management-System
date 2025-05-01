using BloodTypess.Business.Interfaces;
using BloodTypess.Business.Services;
using BloodTypess.Core.Configurations;
using BloodTypess.DataAccess.Interfaces;
using BloodTypess.DataAccess.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using BloodTypess.DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
 using Microsoft.Extensions.DependencyInjection;
using BloodTypess.Core.Models;
using System;

namespace BloodTypess.Web.Extensions
{
	public static class DependencyInjection
	{
		 
		public static void RegisterServices(this IServiceCollection services , IConfiguration configuration )
		{
			services.AddHttpClient();
			services.AddScoped<IBloodTypeService, BloodTypeService>();
			services.AddTransient<IPasswordHasher, PasswordHasher>();
			services.AddScoped<IUserService, UserService>();
 			 



			services.AddScoped<IBloodTypeApiService, BloodTypeApiService>();

			// Bind BloodTypeApiOptions to the "BloodTypeApi" section in appsettings.json
			services.Configure<BloodTypeApiOptions>(configuration.GetSection("BloodTypeApi"));





			// registiration for ApplicationDbContext as Scopped 
			services.AddDbContext<ApplicationDbContext>(cfg => cfg.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));

			services.AddIdentity<AppUser , IdentityRole>(options => {
				// Configure identity options
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequiredLength = 8;
				options.User.RequireUniqueEmail = true;
			})
					.AddEntityFrameworkStores<ApplicationDbContext>()
					.AddDefaultTokenProviders();

			// Configure authentication cookie
			services.ConfigureApplicationCookie(options => {
				options.LoginPath = "/Account/Login";
				options.LogoutPath = "/Home/Index";
				options.AccessDeniedPath = "/Home/AccessDenied";
				options.SlidingExpiration = true;
				options.ExpireTimeSpan = TimeSpan.FromDays(1);
			});



			//// Add ASP.NET Core Identity
			//// this sets up the full Identity system with user/role support, EF Core storage, and token generation.
			//services.AddIdentity<IdentityUser, IdentityRole>()
			//	.AddEntityFrameworkStores<ApplicationDbContext>()
			//	.AddDefaultTokenProviders();

			//// Configure Identity options
			//services.Configure<IdentityOptions>(options =>
			//{
			//	options.Password.RequireDigit = true;
			//	options.Password.RequireLowercase = true;
			//	options.Password.RequireUppercase = true;
			//	options.Password.RequireNonAlphanumeric = false;
			//	options.Password.RequiredLength = 6;
			//});


			//// Configure cookie-based authentication
			//services.ConfigureApplicationCookie(options =>
			//{
			//	options.LoginPath = "/Account/Login";  // Redirect to login page if not authenticated
			//	options.AccessDeniedPath = "/Account/AccessDenied";  // Redirect if not authorized
			//});



			//// configure identity for AppUser Model
			//services.AddIdentity<AppUser, IdentityRole>()
			//		.AddEntityFrameworkStores<ApplicationDbContext>()
			//		.AddDefaultTokenProviders();








			
			// Enforce authentication globally for all pages , controllers and actions
			// use [AllowAnonymous] to allow specific controllers and actions (page) to be accessed without authentication
			services.AddControllersWithViews(options =>
			{
				var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();
				options.Filters.Add(new AuthorizeFilter(policy));
			});
			

			//services.AddMvc();


		}
	}
}
