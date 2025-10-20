using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace BloodTypess.Web.Filters
{
	public class RoleBasedHangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
	{
		public bool Authorize([NotNull] DashboardContext context)
		{
			var httpContext = context.GetHttpContext(); 
			return httpContext.User.Identity?.IsAuthenticated == true && httpContext.User.IsInRole("Admin");
		}
	}
}
