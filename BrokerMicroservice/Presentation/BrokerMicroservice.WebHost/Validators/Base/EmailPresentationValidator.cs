using BrokerMicroservise.ValueObgect.Validators;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Base
{
    public class EmailPresentationValidator : AbstractValidator<string>
    {
        public EmailPresentationValidator()
        {
            RuleFor(request => request)
                .NotNull()
                .NotEmpty();
        }
    }
}
