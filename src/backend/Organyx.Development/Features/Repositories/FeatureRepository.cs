using Organyx.Infrastructure.Tables;
using Organyx.Infrastructure.Services;
using Supabase.Postgrest;

namespace Organyx.Development.Features.Repositories;

public interface IFeatureRepository
{
    Task<IReadOnlyList<Feature>> GetByProjectIdAsync(Guid projectId);
    Task<Feature?> GetByIdAsync(Guid featureId);
    Task<Feature?> GetByProjectIdAndSlugAsync(Guid projectId, string slug);
    Task<Feature?> InsertAsync(Feature feature);
    Task<Feature?> UpdateAsync(Guid featureId, string name, string? description, Guid? statusId);
    Task DeleteAsync(Guid featureId);
}

public class FeatureRepository(SupabaseService supabaseService) : IFeatureRepository
{
    public async Task<IReadOnlyList<Feature>> GetByProjectIdAsync(Guid projectId)
    {
        var result = await supabaseService.Client.From<Feature>()
            .Where(x => x.ProjectId == projectId)
            .Order(x => x.Name, Constants.Ordering.Ascending)
            .Get();
        return result.Models;
    }

    public async Task<Feature?> GetByIdAsync(Guid featureId)
    {
        var result = await supabaseService.Client.From<Feature>()
            .Where(x => x.Id == featureId)
            .Get();
        return result.Models.FirstOrDefault();
    }

    public async Task<Feature?> GetByProjectIdAndSlugAsync(Guid projectId, string slug)
    {
        var result = await supabaseService.Client.From<Feature>()
            .Where(x => x.ProjectId == projectId)
            .Where(x => x.Slug == slug)
            .Get();
        return result.Models.FirstOrDefault();
    }

    public async Task<Feature?> InsertAsync(Feature feature)
    {
        var result = await supabaseService.Client.From<Feature>().Insert(feature);
        return result.Model;
    }

    public async Task<Feature?> UpdateAsync(Guid featureId, string name, string? description, Guid? statusId)
    {
        var result = await supabaseService.Client.From<Feature>()
            .Where(x => x.Id == featureId)
            .Set(x => x.Name, name)
            .Set(x => x.Description!, description)
            .Set(x => x.StatusId!, statusId)
            .Update();
        return result.Models.FirstOrDefault();
    }

    public async Task DeleteAsync(Guid featureId)
    {
        await supabaseService.Client.From<Feature>()
            .Where(x => x.Id == featureId)
            .Delete();
    }
}
