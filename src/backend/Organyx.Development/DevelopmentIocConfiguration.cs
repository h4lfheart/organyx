using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Organyx.Development.Features.Repositories;
using Organyx.Development.Features.Services;
using Organyx.Development.Projects.Validators;
using Organyx.Development.Projects.Repositories;
using Organyx.Development.Projects.Services;
using Organyx.Development.Statuses.Repositories;
using Organyx.Development.Statuses.Services;
using Organyx.Development.Tasks.Repositories;
using Organyx.Development.Tasks.Services;

namespace Organyx.Development;

public static class DevelopmentIocConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDevelopmentIoc()
        {
            services.AddValidatorsFromAssemblyContaining<CreateProjectRequestValidator>();

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