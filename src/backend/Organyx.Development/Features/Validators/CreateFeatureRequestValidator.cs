using FluentValidation;
using Organyx.Development.Features.Models;

namespace Organyx.Development.Features.Validators;

public sealed class CreateFeatureRequestValidator : AbstractValidator<CreateFeatureRequest>
{
    public CreateFeatureRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
