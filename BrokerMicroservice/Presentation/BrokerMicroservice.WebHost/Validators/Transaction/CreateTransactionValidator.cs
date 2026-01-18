using BrokerMicroservice.Application.Models.Transaction;
using BrokerMicroservice.WebHost.Requests.Transaction;
using BrokerMicroservice.WebHost.Validators.Base;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Broker
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionRequest>
    {
        public CreateTransactionValidator()
        {

            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
           
            RuleFor(x => x.ClientId).SetValidator(new GuidPresentationValidator());

            RuleFor(x => x.Type).IsInEnum();

            // Purchase / Sale: AssetId и Quantity
            When(x => x.Type == TransactionType.Purchase || x.Type == TransactionType.Sale, () =>
            {
                RuleFor(x => x.AssetId).NotNull().NotEmpty();
                RuleFor(x => x.Quantity).NotNull().GreaterThan(0);
                // Amount можно не требовать, если у тебя сервис сам считает/не использует
            });

            // DEPOSIT / WITHDRAW: нужен Amount, AssetId/Quantity не нужны
            When(x => x.Type == TransactionType.Replenishment || x.Type == TransactionType.Removing, () =>
            {
                RuleFor(x => x.Amount).GreaterThan(0);
                RuleFor(x => x.AssetId).Null();
                RuleFor(x => x.Quantity).Null();
            });
        }
    }
}
