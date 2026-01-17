using BrokerMicroservice.WebHost.Requests.Broker;
using BrokerMicroservice.WebHost.Validators.Base;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Broker
{
    public class CreateBrokerValidator : AbstractValidator<CreateBrokerRequest>
    {
        public CreateBrokerValidator()
        {
            RuleFor(broker => broker.Name)
                .SetValidator(new BrokerNamePresentationValidator());
        }
    }
}
