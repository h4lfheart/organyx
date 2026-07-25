using FluentValidation;
using Organyx.Development.Tasks.Models;

namespace Organyx.Development.Tasks.Validators;

public sealed class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.StatusId).NotEmpty();
    }
}
