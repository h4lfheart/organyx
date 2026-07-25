using Organyx.Infrastructure.Services;
using Organyx.Infrastructure.Tables;
using Supabase.Postgrest;

namespace Organyx.Application.Projects.Repositories;

public interface IProjectRepository
{
    Task<IReadOnlyList<Project>> GetAllAsync();
}

public class ProjectRepository(SupabaseService supabaseService) : IProjectRepository
{
    public async Task<IReadOnlyList<Project>> GetAllAsync()
    {
        var result = await supabaseService.Client.From<Project>()
            .Order(x => x.Key, Constants.Ordering.Ascending)
            .Get();
        return result.Models;
    }
}
