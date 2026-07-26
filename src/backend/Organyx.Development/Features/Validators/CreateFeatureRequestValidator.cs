using FluentValidation;
using Organyx.Development.Features.Models;

namespace Organyx.Development.Features.Validators;

public sealed class CreateFeatureRequestValidator : AbstractValidator<CreateFeatureRequest>
{
    public CreateFeatureRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(64)
            .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$");
    }
}
