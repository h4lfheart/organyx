using Organyx.Infrastructure.Services;
using Organyx.Infrastructure.Tables;
using Supabase.Postgrest;

namespace Organyx.Application.Features.Repositories;

public interface IFeatureRepository
{
    Task<IReadOnlyList<Feature>> GetByProjectIdAsync(Guid projectId);
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
}
