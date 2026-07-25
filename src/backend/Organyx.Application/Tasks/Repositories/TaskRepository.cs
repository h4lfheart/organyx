using Organyx.Infrastructure.Services;
using Organyx.Infrastructure.Tables;
using Supabase.Postgrest;

namespace Organyx.Application.Tasks.Repositories;

public interface ITaskRepository
{
    Task<IReadOnlyList<ProjectTask>> GetByProjectIdAsync(Guid projectId);
}

public class TaskRepository(SupabaseService supabaseService) : ITaskRepository
{
    public async Task<IReadOnlyList<ProjectTask>> GetByProjectIdAsync(Guid projectId)
    {
        var result = await supabaseService.Client.From<ProjectTask>()
            .Where(x => x.ProjectId == projectId)
            .Order(x => x.Number, Constants.Ordering.Ascending)
            .Get();
        return result.Models;
    }
}
