using Organyx.Application.Features.Models;
using Organyx.Application.Features.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Organyx.Application.Features;

[ApiController]
[Route("projects/{projectId}/features")]
[Tags("Features")]
public class FeaturesController(IFeatureService featureService) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("List Features")]
    public async Task<ActionResult<FeaturesResponse>> GetFeaturesAsync([FromRoute] string projectId)
    {
        var features = await featureService.GetFeaturesAsync(projectId);
        return features is not null ? Ok(features) : NotFound();
    }

    [HttpGet("{featureId}")]
    [EndpointSummary("Get Feature")]
    public async Task<ActionResult<FeatureResponseEntry>> GetFeatureAsync(
        [FromRoute] string projectId,
        [FromRoute] string featureId)
    {
        var feature = await featureService.GetFeatureAsync(projectId, featureId);
        return feature is not null ? Ok(feature) : NotFound();
    }

    [HttpPost]
    [EndpointSummary("Create Feature")]
    public async Task<ActionResult<FeatureResponseEntry>> CreateFeatureAsync(
        [FromRoute] string projectId,
        [FromBody] CreateFeatureRequest request)
    {
        var (status, feature) = await featureService.CreateAsync(projectId, request);
        return feature is not null ? StatusCode((int) status, feature) : StatusCode((int) status);
    }

    [HttpPut("{featureId}")]
    [EndpointSummary("Update Feature")]
    public async Task<ActionResult<FeatureResponseEntry>> UpdateFeatureAsync(
        [FromRoute] string projectId,
        [FromRoute] string featureId,
        [FromBody] UpdateFeatureRequest request)
    {
        var (status, feature) = await featureService.UpdateAsync(projectId, featureId, request);
        return feature is not null ? Ok(feature) : StatusCode((int) status);
    }

    [HttpDelete("{featureId}")]
    [EndpointSummary("Delete Feature")]
    public async Task<ActionResult> DeleteFeatureAsync(
        [FromRoute] string projectId,
        [FromRoute] string featureId)
    {
        return StatusCode((int) await featureService.DeleteAsync(projectId, featureId));
    }
}
