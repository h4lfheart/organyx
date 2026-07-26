using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organyx.Application.Features.Models;
using Organyx.Application.Features.Services;

namespace Organyx.Application.Features;

[ApiController]
[Route("projects/{projectSlug}/features")]
[Tags("Features")]
[ApiExplorerSettings(GroupName = "application")]
public class FeaturesController(IFeatureService featureService) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("List Features")]
    public async Task<ActionResult<FeaturesResponse>> GetFeaturesAsync([FromRoute] string projectSlug)
    {
        return Ok(await featureService.GetFeaturesAsync(projectSlug));
    }
}
