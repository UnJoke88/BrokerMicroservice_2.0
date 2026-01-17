using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Base
{
    public class GuidPresentationValidator : AbstractValidator<Guid>
    {
        public GuidPresentationValidator()
        {
            RuleFor(x => x)
                .NotEmpty();
        }
    }
}
