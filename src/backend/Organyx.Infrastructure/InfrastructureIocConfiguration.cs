using Microsoft.Extensions.DependencyInjection;
using Organyx.Infrastructure.Services;

namespace Organyx.Infrastructure;

public static class InfrastructureIocConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructureIoc()
        {
            services.AddSingleton<SupabaseService>();
            return services;
        }
    }
}
