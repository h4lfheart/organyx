using Microsoft.Extensions.DependencyInjection;
using Organyx.Application.Projects.Repositories;
using Organyx.Application.Projects.Services;

namespace Organyx.Application;

public static class ApplicationIocConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationIoc()
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();

            return services;
        }
    }
}
