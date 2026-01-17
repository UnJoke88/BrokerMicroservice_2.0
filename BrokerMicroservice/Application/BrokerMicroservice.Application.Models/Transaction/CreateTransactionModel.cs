using BrokerMicroservice.Application.Models.Base;


namespace BrokerMicroservice.Application.Models.Transaction
{
    public record class CreateTransactionModel(Guid ClientId, DateTime Date, TransactionType Type, Guid? AssetId, int? Quantity, decimal Amount) : ICreateModel;
}
