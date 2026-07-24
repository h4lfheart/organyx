using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organyx.Development.Statuses.Models;
using Organyx.Development.Statuses.Services;

namespace Organyx.Development.Statuses;

[ApiController]
[Tags("Statuses")]
[ApiExplorerSettings(GroupName = "development")]
public class StatusesController(IStatusService statusService) : ControllerBase
{
    [HttpGet("projects/{projectId:guid}/status")]
    [EndpointSummary("List Statuses")]
    public async Task<ActionResult<StatusesResponse>> GetStatusesAsync([FromRoute] Guid projectId)
    {
        return Ok(await statusService.GetStatusesAsync(projectId));
    }

    [HttpGet("status/{statusId:guid}")]
    [EndpointSummary("Get Status")]
    public async Task<ActionResult<StatusResponseEntry>> GetStatusAsync([FromRoute] Guid statusId)
    {
        return Ok(await statusService.GetStatusAsync(statusId));
    }

    [HttpPost("projects/{projectId:guid}/status")]
    [EndpointSummary("Create Status")]
    public async Task<ActionResult<Guid>> CreateStatusAsync(
        [FromRoute] Guid projectId,
        [FromBody] CreateStatusRequest request)
    {
        var id = await statusService.CreateAsync(projectId, request);
        return Created($"/status/{id}", id);
    }

    [HttpPut("status/{statusId:guid}")]
    [EndpointSummary("Update Status")]
    public async Task<IActionResult> UpdateStatusAsync(
        [FromRoute] Guid statusId,
        [FromBody] UpdateStatusRequest request)
    {
        await statusService.UpdateAsync(statusId, request);
        return NoContent();
    }

    [HttpDelete("status/{statusId:guid}")]
    [EndpointSummary("Delete Status")]
    public async Task<IActionResult> DeleteStatusAsync([FromRoute] Guid statusId)
    {
        await statusService.DeleteAsync(statusId);
        return NoContent();
    }
}