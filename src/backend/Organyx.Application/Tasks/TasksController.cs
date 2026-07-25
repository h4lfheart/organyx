using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organyx.Application.Tasks.Models;
using Organyx.Application.Tasks.Services;

namespace Organyx.Application.Tasks;

[ApiController]
[Route("projects/{projectSlug}/tasks")]
[Tags("Tasks")]
[ApiExplorerSettings(GroupName = "application")]
public class TasksController(ITaskService taskService) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("List Tasks")]
    public async Task<ActionResult<TasksResponse>> GetTasksAsync([FromRoute] string projectSlug)
    {
        return Ok(await taskService.GetTasksAsync(projectSlug));
    }
}
