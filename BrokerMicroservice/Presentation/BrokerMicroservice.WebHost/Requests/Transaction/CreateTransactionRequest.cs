using BrokerMicroservice.Application.Models.Transaction;

namespace BrokerMicroservice.WebHost.Requests.Transaction
{
    public record class CreateTransactionRequest
    {
        public Guid ClientId { get; init; }
        public DateTime Date { get; init; } 
        public TransactionType Type { get; init; }

        public Guid? AssetId { get; init; } 
        public int? Quantity { get; init; } 

        public decimal Amount { get; init; }
    }
}
