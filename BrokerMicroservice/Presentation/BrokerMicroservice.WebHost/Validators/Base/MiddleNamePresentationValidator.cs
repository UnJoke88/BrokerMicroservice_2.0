using BrokerMicroservise.ValueObgect.Validators;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Base
{
    public class MiddleNamePresentationValidator : AbstractValidator<string>
    {
        public MiddleNamePresentationValidator()
        {
            RuleFor(request => request)
                .NotEmpty()
                .MaximumLength(FirstNameValidator.MAX_LENGTH);

        }
    }
}
