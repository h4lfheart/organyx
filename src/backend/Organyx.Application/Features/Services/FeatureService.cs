using Organyx.Application.Features.Models;
using Organyx.Application.Features.Repositories;
using Organyx.Application.Projects.Repositories;
using Organyx.Application.Statuses.Repositories;
using Organyx.Infrastructure.Errors;
using Organyx.Infrastructure.Tables;

namespace Organyx.Application.Features.Services;

public interface IFeatureService
{
    Task<FeaturesResponse> GetFeaturesAsync(string projectSlug);
}

public class FeatureService(
    IFeatureRepository featureRepository,
    IStatusRepository statusRepository,
    IProjectRepository projectRepository
) : IFeatureService
{
    public async Task<FeaturesResponse> GetFeaturesAsync(string projectSlug)
    {
        var project = await projectRepository.GetBySlugAsync(projectSlug)
                      ?? throw new NotFoundException("Project not found.");

        var featuresTask = featureRepository.GetByProjectIdAsync(project.Id);
        var statusesTask = statusRepository.GetByProjectIdAsync(project.Id);
        await Task.WhenAll(featuresTask, statusesTask);

        var features = await featuresTask;
        var statuses = await statusesTask;
        var statusById = statuses.ToDictionary(status => status.Id);

        return new FeaturesResponse
        {
            Entries = features.Select(feature =>
            {
                Status? status = null;
                if (feature.StatusId is { } statusId)
                {
                    if (!statusById.TryGetValue(statusId, out status))
                        throw new BusinessRuleException($"Feature {feature.Slug} has an invalid status.");
                }

                return feature.ToResponse(status);
            })
        };
    }
}
