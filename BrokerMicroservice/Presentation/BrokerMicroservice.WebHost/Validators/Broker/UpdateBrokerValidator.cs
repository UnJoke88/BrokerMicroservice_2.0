using BrokerMicroservice.WebHost.Requests.Broker;
using BrokerMicroservice.WebHost.Validators.Base;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Broker
{
    public class UpdateBrokerValidator : AbstractValidator<UpdateBrokerRequest>
    {
        public UpdateBrokerValidator()
        {
            RuleFor(broker => broker.Name)
                .SetValidator(new BrokerNamePresentationValidator());
        }
    }
}
