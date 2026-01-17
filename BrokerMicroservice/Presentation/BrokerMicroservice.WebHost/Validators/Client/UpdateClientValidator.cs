using BrokerMicroservice.WebHost.Requests.Client;
using BrokerMicroservice.WebHost.Validators.Base;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Client
{
    public class UpdateClientValidator : AbstractValidator<UpdateClientRequest>
    {
        public UpdateClientValidator()
        {
            RuleFor(client => client.FirstName)
                .SetValidator(new FirstNamePresentationValidator());

            RuleFor(client => client.LastName)
                .SetValidator(new LastNamePresentationValidator());

            RuleFor(client => client.MiddleName)
                .SetValidator(new MiddleNamePresentationValidator());

            RuleFor(client => client.Email)
                .SetValidator(new EmailPresentationValidator());
        }
    }
}
