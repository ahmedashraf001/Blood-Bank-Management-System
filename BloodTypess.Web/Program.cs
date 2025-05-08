using BloodTypess.Business.Interfaces;
using BloodTypess.Web.Extensions;
using Hangfire;
using BloodTypess.Web.Seeders;
using BloodTypess.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// register DI services
builder.Services.RegisterServices(builder.Configuration);
 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Register CustomMiddleWare (Compress Http responses)
app.UseMiddleware<ZstdCompressionMiddleware>();


app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	await RoleSeeder.CreateRolesAsync(services);
}

app.UseHangfireDashboard("/dashboard");

BackgroundJob.Enqueue<IDonorService>(s => s.UpdateAgeAsync());
RecurringJob.AddOrUpdate<IDonorService>(
	s => s.UpdateAgeAsync(),
	Cron.Weekly);

BackgroundJob.Enqueue<IDonorService>(s => s.UpdateIsEiligibleToDonateAsync());
RecurringJob.AddOrUpdate<IDonorService>(
	s => s.UpdateIsEiligibleToDonateAsync(),
	Cron.Daily);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
