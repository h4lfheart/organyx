using FluentValidation;
using Organyx.Development.Projects.Models;

namespace Organyx.Development.Projects.Validators;

public sealed class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Key).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
