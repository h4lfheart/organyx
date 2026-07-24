using FluentValidation;
using Organyx.Development.Features.Models;

namespace Organyx.Development.Features.Validators;

public sealed class UpdateFeatureRequestValidator : AbstractValidator<UpdateFeatureRequest>
{
    public UpdateFeatureRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
