using Organyx.Application.Features.Tables;
using Organyx.Infrastructure.Services;
using Supabase.Postgrest;

namespace Organyx.Application.Features.Repositories;

public interface IFeatureRepository
{
    Task<IReadOnlyList<Feature>> GetByProjectIdAsync(string projectId);
    Task<Feature?> GetByIdAsync(string featureId);
    Task<Feature?> InsertAsync(Feature feature);
    Task<Feature?> UpdateAsync(string featureId, string name, string? description);
    Task DeleteAsync(string featureId);
}

public class FeatureRepository(SupabaseService supabaseService) : IFeatureRepository
{
    public async Task<IReadOnlyList<Feature>> GetByProjectIdAsync(string projectId)
    {
        var result = await supabaseService.Client.From<Feature>()
            .Where(x => x.ProjectId == projectId)
            .Order(x => x.Name, Constants.Ordering.Ascending)
            .Get();
        return result.Models;
    }

    public async Task<Feature?> GetByIdAsync(string featureId)
    {
        var result = await supabaseService.Client.From<Feature>()
            .Where(x => x.Id == featureId)
            .Get();
        return result.Models.FirstOrDefault();
    }

    public async Task<Feature?> InsertAsync(Feature feature)
    {
        var result = await supabaseService.Client.From<Feature>().Insert(feature);
        return result.Model;
    }

    public async Task<Feature?> UpdateAsync(string featureId, string name, string? description)
    {
        var result = await supabaseService.Client.From<Feature>()
            .Where(x => x.Id == featureId)
            .Set(x => x.Name, name)
            .Set(x => x.Description!, description)
            .Update();
        return result.Models.FirstOrDefault();
    }

    public async Task DeleteAsync(string featureId)
    {
        await supabaseService.Client.From<Feature>()
            .Where(x => x.Id == featureId)
            .Delete();
    }
}
