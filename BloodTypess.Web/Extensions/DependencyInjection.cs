using BloodTypess.Business.Interfaces;
using BloodTypess.Business.Services;
using BloodTypess.Core.Configurations;
using BloodTypess.DataAccess.Interfaces;
using BloodTypess.DataAccess.Services;
using Microsoft.Extensions.Configuration;

namespace BloodTypess.Web.Extensions
{
	public static class DependencyInjection
	{
		public static void RegisterServices(this IServiceCollection services , IConfiguration configuration)
		{
			services.AddHttpClient();
			services.AddScoped<IBloodTypeService, BloodTypeService>();
 			services.AddScoped<IBloodTypeApiService, BloodTypeApiService>();

			// Bind BloodTypeApiOptions to the "BloodTypeApi" section in appsettings.json
			services.Configure<BloodTypeApiOptions>(configuration.GetSection("BloodTypeApi"));
		}
	}
}
