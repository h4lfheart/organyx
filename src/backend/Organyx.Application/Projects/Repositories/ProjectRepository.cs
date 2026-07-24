using Organyx.Application.Projects.Tables;
using Organyx.Infrastructure.Services;
using Supabase.Postgrest;

namespace Organyx.Application.Projects.Repositories;

public interface IProjectRepository
{
    Task<IReadOnlyList<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(string projectId);
    Task<Project?> GetByKeyAsync(string key);
    Task<Project?> InsertAsync(Project project);
    Task<Project?> UpdateAsync(string projectId, string name, string? description);
    Task DeleteAsync(string projectId);
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

    public async Task<Project?> GetByIdAsync(string projectId)
    {
        var result = await supabaseService.Client.From<Project>()
            .Where(x => x.Id == projectId)
            .Get();
        return result.Models.FirstOrDefault();
    }

    public async Task<Project?> GetByKeyAsync(string key)
    {
        var result = await supabaseService.Client.From<Project>()
            .Where(x => x.Key == key)
            .Get();
        return result.Models.FirstOrDefault();
    }

    public async Task<Project?> InsertAsync(Project project)
    {
        var result = await supabaseService.Client.From<Project>().Insert(project);
        return result.Model;
    }

    public async Task<Project?> UpdateAsync(string projectId, string name, string? description)
    {
        var result = await supabaseService.Client.From<Project>()
            .Where(x => x.Id == projectId)
            .Set(x => x.Name, name)
            .Set(x => x.Description!, description)
            .Update();
        return result.Models.FirstOrDefault();
    }

    public async Task DeleteAsync(string projectId)
    {
        await supabaseService.Client.From<Project>()
            .Where(x => x.Id == projectId)
            .Delete();
    }
}
