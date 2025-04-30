using BloodTypess.Business.Interfaces;
using BloodTypess.Business.Services;
using BloodTypess.DataAccess.Interfaces;
using BloodTypess.DataAccess.Services;

namespace BloodTypess.Web.Extensions
{
	public static class DependencyInjection
	{
		public static void RegisterServices(this IServiceCollection services)
		{
			services.AddHttpClient();
			services.AddScoped<IBloodTypeService, BloodTypeService>();
 			services.AddScoped<IBloodTypeApiService, BloodTypeApiService>();
		}
	}
}
