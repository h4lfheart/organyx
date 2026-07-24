using FluentValidation;
using Organyx.Development.Statuses.Models;

namespace Organyx.Development.Statuses.Validators;

public sealed class CreateStatusRequestValidator : AbstractValidator<CreateStatusRequest>
{
    public CreateStatusRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
