using BrokerMicroservise.ValueObgect.Validators;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Base
{
    public class PhoneNumberPresentationValidator : AbstractValidator<string>
    {
        public PhoneNumberPresentationValidator()
        {
            RuleFor(request => request)
                .NotNull()
                .NotEmpty()
                .MaximumLength(PhoneNumberValidator.REQUIRED_LENGTH);
        }
    }
}
