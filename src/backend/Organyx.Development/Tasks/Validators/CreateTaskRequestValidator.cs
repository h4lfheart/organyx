using FluentValidation;
using Organyx.Development.Tasks.Models;

namespace Organyx.Development.Tasks.Validators;

public sealed class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}
