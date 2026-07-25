using Organyx.Infrastructure.Services;
using Organyx.Infrastructure.Tables;
using Supabase.Postgrest;

namespace Organyx.Application.Statuses.Repositories;

public interface IStatusRepository
{
    Task<IReadOnlyList<Status>> GetByProjectIdAsync(Guid projectId);
}

public class StatusRepository(SupabaseService supabaseService) : IStatusRepository
{
    public async Task<IReadOnlyList<Status>> GetByProjectIdAsync(Guid projectId)
    {
        var result = await supabaseService.Client.From<Status>()
            .Where(x => x.ProjectId == projectId)
            .Order(x => x.Position, Constants.Ordering.Ascending)
            .Get();
        return result.Models;
    }
}
