using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organyx.Development.Features.Models;
using Organyx.Development.Features.Services;

namespace Organyx.Development.Features;

[ApiController]
[Tags("Features")]
[ApiExplorerSettings(GroupName = "development")]
public class FeaturesController(IFeatureService featureService) : ControllerBase
{
    [HttpGet("projects/{projectId:guid}/features")]
    [EndpointSummary("List Features")]
    public async Task<ActionResult<FeaturesResponse>> GetFeaturesAsync([FromRoute] Guid projectId)
    {
        return Ok(await featureService.GetFeaturesAsync(projectId));
    }

    [HttpGet("features/{featureId:guid}")]
    [EndpointSummary("Get Feature")]
    public async Task<ActionResult<FeatureResponseEntry>> GetFeatureAsync([FromRoute] Guid featureId)
    {
        return Ok(await featureService.GetFeatureAsync(featureId));
    }

    [HttpPost("projects/{projectId:guid}/features")]
    [EndpointSummary("Create Feature")]
    public async Task<ActionResult<Guid>> CreateFeatureAsync(
        [FromRoute] Guid projectId,
        [FromBody] CreateFeatureRequest request)
    {
        var id = await featureService.CreateAsync(projectId, request);
        return CreatedAtAction(nameof(GetFeatureAsync), new { featureId = id }, id);
    }

    [HttpPut("features/{featureId:guid}")]
    [EndpointSummary("Update Feature")]
    public async Task<IActionResult> UpdateFeatureAsync(
        [FromRoute] Guid featureId,
        [FromBody] UpdateFeatureRequest request)
    {
        await featureService.UpdateAsync(featureId, request);
        return NoContent();
    }

    [HttpDelete("features/{featureId:guid}")]
    [EndpointSummary("Delete Feature")]
    public async Task<IActionResult> DeleteFeatureAsync([FromRoute] Guid featureId)
    {
        await featureService.DeleteAsync(featureId);
        return NoContent();
    }
}
