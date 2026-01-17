using BrokerMicroservice.WebHost.Requests.Broker;
using BrokerMicroservice.WebHost.Requests.Transaction;
using BrokerMicroservice.WebHost.Validators.Base;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Broker
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionRequest>
    {
        public CreateTransactionValidator()
        {
            RuleFor(transaction => transaction.ClientId)
                .SetValidator(new GuidPresentationValidator());

            RuleFor(transaction => transaction.Date).NotNull().NotEmpty();

            RuleFor(transaction => transaction.Type).IsInEnum().WithMessage("Не существует перечисления");

            RuleFor(transaction => transaction.Quantity).GreaterThanOrEqualTo(0);

            RuleFor(transaction => transaction.Amount)
                .SetValidator(new MoneyAmountPresentationValidator());
        }
    }
}
