using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organyx.Application.Projects.Models;
using Organyx.Application.Projects.Services;

namespace Organyx.Application.Projects;

[ApiController]
[Route("projects")]
[Tags("Projects")]
[ApiExplorerSettings(GroupName = "application")]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("List Projects")]
    public async Task<ActionResult<ProjectsResponse>> GetProjectsAsync()
    {
        return Ok(await projectService.GetProjectsAsync());
    }
}
