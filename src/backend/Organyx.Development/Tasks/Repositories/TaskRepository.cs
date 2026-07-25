using Organyx.Infrastructure.Tables;
using Organyx.Infrastructure.Services;
using Supabase.Postgrest;
using Supabase.Postgrest.Exceptions;

namespace Organyx.Development.Tasks.Repositories;

public interface ITaskRepository
{
    Task<IReadOnlyList<ProjectTask>> GetByProjectIdAsync(
        Guid projectId,
        Guid? featureId,
        Guid? statusId,
        string? priority,
        string? search);

    Task<ProjectTask?> GetByIdAsync(Guid taskId);
    Task<ProjectTask?> InsertAsync(ProjectTask task);

    Task<ProjectTask?> UpdateAsync(
        Guid taskId,
        string title,
        string? description,
        string priority,
        Guid? featureId,
        Guid statusId);

    Task DeleteAsync(Guid taskId);
}

public class TaskRepository(SupabaseService supabaseService) : ITaskRepository
{
    public async Task<IReadOnlyList<ProjectTask>> GetByProjectIdAsync(
        Guid projectId,
        Guid? featureId,
        Guid? statusId,
        string? priority,
        string? search)
    {
        var query = supabaseService.Client.From<ProjectTask>()
            .Where(x => x.ProjectId == projectId);

        if (featureId is not null)
            query = query.Where(x => x.FeatureId == featureId);

        if (statusId is not null)
            query = query.Where(x => x.StatusId == statusId);

        if (!string.IsNullOrWhiteSpace(priority))
            query = query.Where(x => x.Priority == priority);

        var result = await query
            .Order(x => x.Number, Constants.Ordering.Ascending)
            .Get();

        IEnumerable<ProjectTask> tasks = result.Models;
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            tasks = tasks.Where(task =>
                task.Title.Contains(term, StringComparison.OrdinalIgnoreCase)
                || (task.Description?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false)
                || task.Number.ToString().Equals(term, StringComparison.OrdinalIgnoreCase));
        }

        return tasks.ToList();
    }

    public async Task<ProjectTask?> GetByIdAsync(Guid taskId)
    {
        var result = await supabaseService.Client.From<ProjectTask>()
            .Where(x => x.Id == taskId)
            .Get();
        return result.Models.FirstOrDefault();
    }

    public async Task<ProjectTask?> InsertAsync(ProjectTask task)
    {
        try
        {
            var result = await supabaseService.Client.From<ProjectTask>().Insert(task);
            return result.Model;
        }
        catch (PostgrestException)
        {
            return null;
        }
    }

    public async Task<ProjectTask?> UpdateAsync(
        Guid taskId,
        string title,
        string? description,
        string priority,
        Guid? featureId,
        Guid statusId)
    {
        var result = await supabaseService.Client.From<ProjectTask>()
            .Where(x => x.Id == taskId)
            .Set(x => x.Title, title)
            .Set(x => x.Description!, description)
            .Set(x => x.Priority, priority)
            .Set(x => x.FeatureId!, featureId)
            .Set(x => x.StatusId, statusId)
            .Update();
        return result.Models.FirstOrDefault();
    }

    public async Task DeleteAsync(Guid taskId)
    {
        await supabaseService.Client.From<ProjectTask>()
            .Where(x => x.Id == taskId)
            .Delete();
    }
}