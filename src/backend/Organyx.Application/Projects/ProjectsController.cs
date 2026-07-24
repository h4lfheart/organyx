using Organyx.Application.Projects.Models;
using Organyx.Application.Projects.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Organyx.Application.Projects;

[ApiController]
[Route("projects")]
[Tags("Projects")]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("List Projects")]
    public async Task<ActionResult<ProjectsResponse>> GetProjectsAsync()
    {
        return Ok(await projectService.GetProjectsAsync());
    }

    [HttpGet("{projectId}")]
    [EndpointSummary("Get Project")]
    public async Task<ActionResult<ProjectResponseEntry>> GetProjectAsync([FromRoute] string projectId)
    {
        var project = await projectService.GetProjectAsync(projectId);
        return project is not null ? Ok(project) : NotFound();
    }

    [HttpPost]
    [EndpointSummary("Create Project")]
    public async Task<ActionResult<ProjectResponseEntry>> CreateProjectAsync([FromBody] CreateProjectRequest request)
    {
        var (status, project) = await projectService.CreateAsync(request);
        return project is not null ? StatusCode((int) status, project) : StatusCode((int) status);
    }

    [HttpPut("{projectId}")]
    [EndpointSummary("Update Project")]
    public async Task<ActionResult<ProjectResponseEntry>> UpdateProjectAsync(
        [FromRoute] string projectId,
        [FromBody] UpdateProjectRequest request)
    {
        var (status, project) = await projectService.UpdateAsync(projectId, request);
        return project is not null ? Ok(project) : StatusCode((int) status);
    }

    [HttpDelete("{projectId}")]
    [EndpointSummary("Delete Project")]
    public async Task<ActionResult> DeleteProjectAsync([FromRoute] string projectId)
    {
        return StatusCode((int) await projectService.DeleteAsync(projectId));
    }
}
