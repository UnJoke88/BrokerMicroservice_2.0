using BrokerMicroservise.ValueObgect.Validators;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Base
{
    public class BrokerNamePresentationValidator : AbstractValidator<string>
    {
        public BrokerNamePresentationValidator()
        {
            RuleFor(request => request)
                .NotNull()
                .NotEmpty()
                .MinimumLength(BrokerNameValidator.MIN_LENGTH)
                .MaximumLength(BrokerNameValidator.MAX_LENGTH);
        }
    }
}
