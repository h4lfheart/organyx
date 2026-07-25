using Microsoft.Extensions.DependencyInjection;
using Organyx.Application.Projects.Repositories;
using Organyx.Application.Projects.Services;
using Organyx.Application.Statuses.Repositories;
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

            services.AddScoped<IStatusRepository, StatusRepository>();

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}
