using BrokerMicroservice.WebHost.Requests.Broker;
using BrokerMicroservise.ValueObgect.Validators;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Base
{
    public class FirstNamePresentationValidator : AbstractValidator<string>
    {
        public FirstNamePresentationValidator()
        {
            RuleFor(request => request)
                .NotNull()
                .NotEmpty()
                .MinimumLength(FirstNameValidator.MIN_LENGTH)
                .MaximumLength(FirstNameValidator.MAX_LENGTH);
    
        }
    }
}
