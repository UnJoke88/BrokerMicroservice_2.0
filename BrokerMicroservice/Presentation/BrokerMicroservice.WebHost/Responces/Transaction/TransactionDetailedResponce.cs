using BrokerMicroservice.Application.Models.Transaction;


namespace BrokerMicroservice.WebHost.Responces.Transaction
{
    public record class TransactionDetailedResponce(Guid Id, Guid ClientId, DateTime Date, TransactionType Type, Guid? AssetId, int? Quantity, decimal Amount,
        TransactionStatus Status, decimal EndBalance)
    {
    }
}
