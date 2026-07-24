using Organyx.Development.Features.Models;
using Organyx.Development.Features.Repositories;
using Organyx.Development.Projects.Repositories;
using Organyx.Infrastructure.Errors;

namespace Organyx.Development.Features.Services;

public interface IFeatureService
{
    Task<FeaturesResponse> GetFeaturesAsync(Guid projectId);
    Task<FeatureResponseEntry> GetFeatureAsync(Guid featureId);
    Task<Guid> CreateAsync(Guid projectId, CreateFeatureRequest request);
    Task UpdateAsync(Guid featureId, UpdateFeatureRequest request);
    Task DeleteAsync(Guid featureId);
}

public class FeatureService(
    IFeatureRepository featureRepository,
    IProjectRepository projectRepository
) : IFeatureService
{
    public async Task<FeaturesResponse> GetFeaturesAsync(Guid projectId)
    {
        if (await projectRepository.GetByIdAsync(projectId) is null)
            throw new NotFoundException("Project not found.");

        var features = await featureRepository.GetByProjectIdAsync(projectId);
        return new FeaturesResponse
        {
            Entries = features.Select(feature => feature.ToResponse())
        };
    }

    public async Task<FeatureResponseEntry> GetFeatureAsync(Guid featureId)
    {
        var feature = await featureRepository.GetByIdAsync(featureId)
                      ?? throw new NotFoundException("Feature not found.");
        return feature.ToResponse();
    }

    public async Task<Guid> CreateAsync(Guid projectId, CreateFeatureRequest request)
    {
        if (await projectRepository.GetByIdAsync(projectId) is null)
            throw new NotFoundException("Project not found.");

        var created = await featureRepository.InsertAsync(request.ToTable(projectId))
                      ?? throw new InvalidOperationException("Failed to create feature.");
        return created.Id;
    }

    public async Task UpdateAsync(Guid featureId, UpdateFeatureRequest request)
    {
        if (await featureRepository.GetByIdAsync(featureId) is null)
            throw new NotFoundException("Feature not found.");

        _ = await featureRepository.UpdateAsync(featureId, request.Name, request.Description)
            ?? throw new NotFoundException("Feature not found.");
    }

    public async Task DeleteAsync(Guid featureId)
    {
        if (await featureRepository.GetByIdAsync(featureId) is null)
            throw new NotFoundException("Feature not found.");

        await featureRepository.DeleteAsync(featureId);
    }
}