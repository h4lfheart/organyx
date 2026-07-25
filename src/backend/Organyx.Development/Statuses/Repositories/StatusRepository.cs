using Organyx.Infrastructure.Tables;
using Organyx.Infrastructure.Services;
using Supabase.Postgrest;

namespace Organyx.Development.Statuses.Repositories;

public interface IStatusRepository
{
    Task<IReadOnlyList<Status>> GetByProjectIdAsync(Guid projectId);
    Task<Status?> GetByIdAsync(Guid statusId);
    Task<Status?> GetDefaultByProjectIdAsync(Guid projectId);
    Task ClearDefaultForProjectAsync(Guid projectId, Guid? exceptStatusId = null);
    Task<Status?> InsertAsync(Status status);
    Task<Status?> UpdateAsync(Guid statusId, string name, int position, bool isDefault);
    Task DeleteAsync(Guid statusId);
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

    public async Task<Status?> GetByIdAsync(Guid statusId)
    {
        var result = await supabaseService.Client.From<Status>()
            .Where(x => x.Id == statusId)
            .Get();
        return result.Models.FirstOrDefault();
    }

    public async Task<Status?> GetDefaultByProjectIdAsync(Guid projectId)
    {
        var result = await supabaseService.Client.From<Status>()
            .Where(x => x.ProjectId == projectId)
            .Where(x => x.IsDefault)
            .Get();
        return result.Models.FirstOrDefault();
    }

    public async Task ClearDefaultForProjectAsync(Guid projectId, Guid? exceptStatusId = null)
    {
        var query = supabaseService.Client.From<Status>()
            .Where(x => x.ProjectId == projectId)
            .Where(x => x.IsDefault);

        if (exceptStatusId is not null)
            query = query.Where(x => x.Id != exceptStatusId.Value);

        await query.Set(x => x.IsDefault, false).Update();
    }

    public async Task<Status?> InsertAsync(Status status)
    {
        var result = await supabaseService.Client.From<Status>().Insert(status);
        return result.Model;
    }

    public async Task<Status?> UpdateAsync(Guid statusId, string name, int position, bool isDefault)
    {
        var result = await supabaseService.Client.From<Status>()
            .Where(x => x.Id == statusId)
            .Set(x => x.Name, name)
            .Set(x => x.Position, position)
            .Set(x => x.IsDefault, isDefault)
            .Update();
        return result.Models.FirstOrDefault();
    }

    public async Task DeleteAsync(Guid statusId)
    {
        await supabaseService.Client.From<Status>()
            .Where(x => x.Id == statusId)
            .Delete();
    }
}
