using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organyx.Development.Tasks.Models;
using Organyx.Development.Tasks.Services;

namespace Organyx.Development.Tasks;

[ApiController]
[Tags("Tasks")]
[ApiExplorerSettings(GroupName = "development")]
public class TasksController(ITaskService taskService) : ControllerBase
{
    [HttpGet("projects/{projectId:guid}/tasks")]
    [EndpointSummary("List Tasks")]
    public async Task<ActionResult<TasksResponse>> GetTasksAsync(
        [FromRoute] Guid projectId,
        [FromQuery] Guid? featureId,
        [FromQuery] Guid? statusId,
        [FromQuery] Priority? priority,
        [FromQuery] string? search)
    {
        return Ok(await taskService.GetTasksAsync(projectId, featureId, statusId, priority, search));
    }

    [HttpGet("tasks/{taskId:guid}")]
    [EndpointSummary("Get Task")]
    public async Task<ActionResult<TaskResponseEntry>> GetTaskAsync([FromRoute] Guid taskId)
    {
        return Ok(await taskService.GetTaskAsync(taskId));
    }

    [HttpPost("projects/{projectId:guid}/tasks")]
    [EndpointSummary("Create Task")]
    public async Task<ActionResult<Guid>> CreateTaskAsync(
        [FromRoute] Guid projectId,
        [FromBody] CreateTaskRequest request)
    {
        var id = await taskService.CreateAsync(projectId, request);
        return Created($"/tasks/{id}", id);
    }

    [HttpPut("tasks/{taskId:guid}")]
    [EndpointSummary("Update Task")]
    public async Task<IActionResult> UpdateTaskAsync(
        [FromRoute] Guid taskId,
        [FromBody] UpdateTaskRequest request)
    {
        await taskService.UpdateAsync(taskId, request);
        return NoContent();
    }

    [HttpDelete("tasks/{taskId:guid}")]
    [EndpointSummary("Delete Task")]
    public async Task<IActionResult> DeleteTaskAsync([FromRoute] Guid taskId)
    {
        await taskService.DeleteAsync(taskId);
        return NoContent();
    }
}