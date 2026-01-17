using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Base
{
    public class MinimalUnitPresentationValidator : AbstractValidator<int>
    {
        public MinimalUnitPresentationValidator()
        {
            RuleFor(request => request)
                .NotNull()
                .NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
