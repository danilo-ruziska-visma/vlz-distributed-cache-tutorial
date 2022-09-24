using Microsoft.Extensions.DependencyInjection;
using VismaNmbrs.DistributedCacheSample.Data.Options;

namespace VismaNmbrs.DistributedCacheSample.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAzureCosmo(
            this IServiceCollection services, Action<AzureCosmoOptions> options)
        {
            services.Configure(options);
                
            services.AddScoped(typeof(IAsyncDatabase<>), typeof(AzureCosmoDatabase<>));
            return services;
        }
    }
}
