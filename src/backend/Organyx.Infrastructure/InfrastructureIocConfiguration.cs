using Microsoft.Extensions.DependencyInjection;
using Organyx.Infrastructure.Services;
using Organyx.Infrastructure.Validation;

namespace Organyx.Infrastructure;

public static class InfrastructureIocConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructureIoc()
        {
            services.AddSingleton<SupabaseService>();
            services.AddScoped<FluentValidationActionFilter>();
            return services;
        }
    }
}
