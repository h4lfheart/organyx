using System.Net;
using Organyx.Application.Features.Models;
using Organyx.Application.Features.Repositories;
using Organyx.Application.Projects.Repositories;

namespace Organyx.Application.Features.Services;

public interface IFeatureService
{
    Task<FeaturesResponse?> GetFeaturesAsync(string projectId);
    Task<FeatureResponseEntry?> GetFeatureAsync(string projectId, string featureId);
    Task<(HttpStatusCode Status, FeatureResponseEntry? Feature)> CreateAsync(string projectId, CreateFeatureRequest request);
    Task<(HttpStatusCode Status, FeatureResponseEntry? Feature)> UpdateAsync(string projectId, string featureId, UpdateFeatureRequest request);
    Task<HttpStatusCode> DeleteAsync(string projectId, string featureId);
}

public class FeatureService(
    IFeatureRepository featureRepository,
    IProjectRepository projectRepository
) : IFeatureService
{
    public async Task<FeaturesResponse?> GetFeaturesAsync(string projectId)
    {
        if (await projectRepository.GetByIdAsync(projectId) is null)
            return null;

        var features = await featureRepository.GetByProjectIdAsync(projectId);
        return new FeaturesResponse
        {
            Entries = features.Select(feature => feature.ToResponse())
        };
    }

    public async Task<FeatureResponseEntry?> GetFeatureAsync(string projectId, string featureId)
    {
        var feature = await featureRepository.GetByIdAsync(featureId);
        if (feature is null || feature.ProjectId != projectId)
            return null;

        return feature.ToResponse();
    }

    public async Task<(HttpStatusCode Status, FeatureResponseEntry? Feature)> CreateAsync(string projectId, CreateFeatureRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return (HttpStatusCode.BadRequest, null);

        if (await projectRepository.GetByIdAsync(projectId) is null)
            return (HttpStatusCode.NotFound, null);

        var created = await featureRepository.InsertAsync(request.ToTable(projectId));
        return created is not null
            ? (HttpStatusCode.Created, created.ToResponse())
            : (HttpStatusCode.InternalServerError, null);
    }

    public async Task<(HttpStatusCode Status, FeatureResponseEntry? Feature)> UpdateAsync(
        string projectId,
        string featureId,
        UpdateFeatureRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return (HttpStatusCode.BadRequest, null);

        var existing = await featureRepository.GetByIdAsync(featureId);
        if (existing is null || existing.ProjectId != projectId)
            return (HttpStatusCode.NotFound, null);

        var updated = await featureRepository.UpdateAsync(featureId, request.Name, request.Description);
        return updated is not null
            ? (HttpStatusCode.OK, updated.ToResponse())
            : (HttpStatusCode.NotFound, null);
    }

    public async Task<HttpStatusCode> DeleteAsync(string projectId, string featureId)
    {
        var existing = await featureRepository.GetByIdAsync(featureId);
        if (existing is null || existing.ProjectId != projectId)
            return HttpStatusCode.NotFound;

        await featureRepository.DeleteAsync(featureId);
        return HttpStatusCode.NoContent;
    }
}
