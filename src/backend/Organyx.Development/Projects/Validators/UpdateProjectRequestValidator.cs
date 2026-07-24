using FluentValidation;
using Organyx.Development.Projects.Models;

namespace Organyx.Development.Projects.Validators;

public sealed class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
{
    public UpdateProjectRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
