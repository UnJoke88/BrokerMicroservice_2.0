using BrokerMicroservise.ValueObgect.Validators;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Base
{
    public class LastNamePresentationValidator : AbstractValidator<string>
    {
        public LastNamePresentationValidator()
        {
            RuleFor(request => request)
                .NotNull()
                .NotEmpty()
                .MinimumLength(FirstNameValidator.MIN_LENGTH)
                .MaximumLength(FirstNameValidator.MAX_LENGTH);

        }
    }
}
