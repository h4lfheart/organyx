using Organyx.Infrastructure.Services;
using Organyx.Infrastructure.Tables;
using Supabase.Postgrest;

namespace Organyx.Application.Projects.Repositories;

public interface IProjectRepository
{
    Task<IReadOnlyList<Project>> GetAllAsync();
    Task<Project?> GetBySlugAsync(string slug);
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

    public async Task<Project?> GetBySlugAsync(string slug)
    {
        var result = await supabaseService.Client.From<Project>()
            .Where(x => x.Slug == slug)
            .Get();
        return result.Models.FirstOrDefault();
    }
}
