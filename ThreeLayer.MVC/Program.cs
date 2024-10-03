using Microsoft.EntityFrameworkCore;
using ThreeLayer.Business.Interfaces;
using ThreeLayer.Business.Notifications;
using ThreeLayer.Data.Context;
using ThreeLayer.MVC.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<INotifier, Notifier>();

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseLazyLoadingProxies()
               .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<ConcurrencyMiddleware>();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
