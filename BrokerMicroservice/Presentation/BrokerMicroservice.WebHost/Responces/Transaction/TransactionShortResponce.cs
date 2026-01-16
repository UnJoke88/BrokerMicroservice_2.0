using BrokerMicroservice.Application.Models.Transaction;

namespace BrokerMicroservice.WebHost.Responces.Transaction
{
    public record class TransactionShortResponce(Guid Id, Guid ClientId, DateTime Date, TransactionType Type, decimal Amount, TransactionStatus Status, decimal EndBalance)
    {
    }
}
