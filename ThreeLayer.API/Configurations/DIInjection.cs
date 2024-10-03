using ThreeLayer.Business.Interfaces;
using ThreeLayer.Business.Interfaces.Repository;
using ThreeLayer.Business.Interfaces.Services;
using ThreeLayer.Business.Notifications;
using ThreeLayer.Business.Services;
using ThreeLayer.Data.Context;
using ThreeLayer.Data.Repository;

namespace ThreeLayer.API.Configurations
{
    public static class DIInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<IBrazilianPersonService, BrazilianPersonService>();

            services.AddScoped<IBrazilianPersonRepository, BrazilianPersonRepository>();
            return services;
        }
    }
}
