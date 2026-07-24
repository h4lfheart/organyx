using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organyx.Application.Tasks.Models;
using Organyx.Application.Tasks.Services;

namespace Organyx.Application.Tasks;

[ApiController]
[Route("projects/{projectId}/tasks")]
[Tags("Tasks")]
public class TasksController(ITaskService taskService) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("List Tasks")]
    public async Task<ActionResult<TasksResponse>> GetTasksAsync(
        [FromRoute] string projectId,
        [FromQuery] string? featureId,
        [FromQuery] string? statusId,
        [FromQuery] Priority? priority,
        [FromQuery] string? search)
    {
        var tasks = await taskService.GetTasksAsync(projectId, featureId, statusId, priority, search);
        return tasks is not null ? Ok(tasks) : NotFound();
    }

    [HttpGet("{taskId}")]
    [EndpointSummary("Get Task")]
    public async Task<ActionResult<TaskResponseEntry>> GetTaskAsync(
        [FromRoute] string projectId,
        [FromRoute] string taskId)
    {
        var task = await taskService.GetTaskAsync(projectId, taskId);
        return task is not null ? Ok(task) : NotFound();
    }

    [HttpPost]
    [EndpointSummary("Create Task")]
    public async Task<ActionResult<TaskResponseEntry>> CreateTaskAsync(
        [FromRoute] string projectId,
        [FromBody] CreateTaskRequest request)
    {
        var (status, task) = await taskService.CreateAsync(projectId, request);
        return task is not null ? StatusCode((int) status, task) : StatusCode((int) status);
    }

    [HttpPut("{taskId}")]
    [EndpointSummary("Update Task")]
    public async Task<ActionResult<TaskResponseEntry>> UpdateTaskAsync(
        [FromRoute] string projectId,
        [FromRoute] string taskId,
        [FromBody] UpdateTaskRequest request)
    {
        var (status, task) = await taskService.UpdateAsync(projectId, taskId, request);
        return task is not null ? Ok(task) : StatusCode((int) status);
    }

    [HttpDelete("{taskId}")]
    [EndpointSummary("Delete Task")]
    public async Task<ActionResult> DeleteTaskAsync(
        [FromRoute] string projectId,
        [FromRoute] string taskId)
    {
        return StatusCode((int) await taskService.DeleteAsync(projectId, taskId));
    }
}
