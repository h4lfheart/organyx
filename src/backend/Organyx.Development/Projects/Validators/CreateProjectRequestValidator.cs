using FluentValidation;
using Organyx.Development.Projects.Models;

namespace Organyx.Development.Projects.Validators;

public sealed class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Key).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(64)
            .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$");
    }
}
