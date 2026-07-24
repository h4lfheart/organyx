using Organyx.Application.Statuses.Models;
using Organyx.Application.Statuses.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Organyx.Application.Statuses;

[ApiController]
[Route("projects/{projectId}/statuses")]
[Tags("Statuses")]
public class StatusesController(IStatusService statusService) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("List Statuses")]
    public async Task<ActionResult<StatusesResponse>> GetStatusesAsync([FromRoute] string projectId)
    {
        var statuses = await statusService.GetStatusesAsync(projectId);
        return statuses is not null ? Ok(statuses) : NotFound();
    }

    [HttpGet("{statusId}")]
    [EndpointSummary("Get Status")]
    public async Task<ActionResult<StatusResponseEntry>> GetStatusAsync(
        [FromRoute] string projectId,
        [FromRoute] string statusId)
    {
        var status = await statusService.GetStatusAsync(projectId, statusId);
        return status is not null ? Ok(status) : NotFound();
    }

    [HttpPost]
    [EndpointSummary("Create Status")]
    public async Task<ActionResult<StatusResponseEntry>> CreateStatusAsync(
        [FromRoute] string projectId,
        [FromBody] CreateStatusRequest request)
    {
        var (status, entry) = await statusService.CreateAsync(projectId, request);
        return entry is not null ? StatusCode((int) status, entry) : StatusCode((int) status);
    }

    [HttpPut("{statusId}")]
    [EndpointSummary("Update Status")]
    public async Task<ActionResult<StatusResponseEntry>> UpdateStatusAsync(
        [FromRoute] string projectId,
        [FromRoute] string statusId,
        [FromBody] UpdateStatusRequest request)
    {
        var (status, entry) = await statusService.UpdateAsync(projectId, statusId, request);
        return entry is not null ? Ok(entry) : StatusCode((int) status);
    }

    [HttpDelete("{statusId}")]
    [EndpointSummary("Delete Status")]
    public async Task<ActionResult> DeleteStatusAsync(
        [FromRoute] string projectId,
        [FromRoute] string statusId)
    {
        return StatusCode((int) await statusService.DeleteAsync(projectId, statusId));
    }
}
