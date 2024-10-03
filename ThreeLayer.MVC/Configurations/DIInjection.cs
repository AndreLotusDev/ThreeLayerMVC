using ThreeLayer.Business.Interfaces;
using ThreeLayer.Business.Notifications;
using ThreeLayer.Data.Context;

namespace ThreeLayer.MVC.Configurations
{
    public static class DIInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            services.AddScoped<INotifier, Notifier>();
            return services;
        }
    }
}
