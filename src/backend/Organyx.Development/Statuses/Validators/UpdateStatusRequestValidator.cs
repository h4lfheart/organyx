using FluentValidation;
using Organyx.Development.Statuses.Models;

namespace Organyx.Development.Statuses.Validators;

public sealed class UpdateStatusRequestValidator : AbstractValidator<UpdateStatusRequest>
{
    public UpdateStatusRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
