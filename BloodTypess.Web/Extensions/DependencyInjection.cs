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
using BloodTypess.DataAccess.Repositories;
using Hangfire;
using Hangfire.SqlServer;

namespace BloodTypess.Web.Extensions
{
	public static class DependencyInjection
	{
		 
		public static void RegisterServices(this IServiceCollection services , IConfiguration configuration )
		{
			services.AddHttpClient();

			//Register Services
			services.AddScoped<IBloodTypeService, BloodTypeService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IBloodTypeStockService, BloodTypeStockService>();
			services.AddScoped<IDonorService, DonorService>();
			services.AddScoped<IUserManagementService, UserManagementService>();
			services.AddScoped<IReportService, ReportService>();

			//Register Repositories
			services.AddScoped<IBloodTypeStockRepository, BloodTypeStockRepository>();
			services.AddScoped<IDonorRepository, DonorRepository>();
			services.AddScoped<IBloodTypeRepository, BloodTypeRepository>();
			services.AddScoped<IReportRepository, ReportRepository>();

			//register memory cache
			services.AddMemoryCache();


			services.AddScoped<IBloodTypeApiService, BloodTypeApiService>();

			// Bind BloodTypeApiOptions to the "BloodTypeApi" section in appsettings.json
			services.Configure<BloodTypeApiOptions>(configuration.GetSection("BloodTypeApi"));





			// Add DbContext
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



			 






			// Enforce authentication globally for all pages , controllers and actions
			// use [AllowAnonymous] to allow specific controllers and actions (page) to be accessed without authentication
			services.AddControllersWithViews(options =>
			{
				var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();
				options.Filters.Add(new AuthorizeFilter(policy));
			});

			// Configure Background job Hangfire
 			services.AddHangfire(config =>
				 config.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"),
				 new SqlServerStorageOptions
				 {
					 CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
					 SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
					 QueuePollInterval = TimeSpan.Zero,
					 UseRecommendedIsolationLevel = true,
					 DisableGlobalLocks = true
				 }));

			services.AddHangfireServer();


		}
	}
}
