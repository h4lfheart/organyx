using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organyx.Development.Projects.Models;
using Organyx.Development.Projects.Services;

namespace Organyx.Development.Projects;

[ApiController]
[Route("projects")]
[Tags("Projects")]
[ApiExplorerSettings(GroupName = "development")]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("List Projects")]
    public async Task<ActionResult<ProjectsResponse>> GetProjectsAsync()
    {
        return Ok(await projectService.GetProjectsAsync());
    }

    [HttpGet("{projectId:guid}")]
    [EndpointSummary("Get Project")]
    public async Task<ActionResult<ProjectResponseEntry>> GetProjectAsync([FromRoute] Guid projectId)
    {
        return Ok(await projectService.GetProjectAsync(projectId));
    }

    [HttpPost]
    [EndpointSummary("Create Project")]
    public async Task<ActionResult<Guid>> CreateProjectAsync([FromBody] CreateProjectRequest request)
    {
        var id = await projectService.CreateAsync(request);
        return Created($"/projects/{id}", id);
    }

    [HttpPut("{projectId:guid}")]
    [EndpointSummary("Update Project")]
    public async Task<IActionResult> UpdateProjectAsync(
        [FromRoute] Guid projectId,
        [FromBody] UpdateProjectRequest request)
    {
        await projectService.UpdateAsync(projectId, request);
        return NoContent();
    }

    [HttpDelete("{projectId:guid}")]
    [EndpointSummary("Delete Project")]
    public async Task<IActionResult> DeleteProjectAsync([FromRoute] Guid projectId)
    {
        await projectService.DeleteAsync(projectId);
        return NoContent();
    }
}