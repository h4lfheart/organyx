using Organyx.Application.Features.Repositories;
using Organyx.Application.Features.Services;
using Organyx.Application.Projects.Repositories;
using Organyx.Application.Projects.Services;
using Organyx.Application.Statuses.Repositories;
using Organyx.Application.Statuses.Services;
using Microsoft.Extensions.DependencyInjection;
using Organyx.Application.Tasks.Repositories;
using Organyx.Application.Tasks.Services;

namespace Organyx.Application;

public static class ApplicationIocConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationIoc()
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();

            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IFeatureService, FeatureService>();

            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IStatusService, StatusService>();

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}
