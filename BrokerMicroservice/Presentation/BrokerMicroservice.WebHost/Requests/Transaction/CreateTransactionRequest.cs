namespace BrokerMicroservice.WebHost.Requests.Transaction
{
    public record class CreateTransactionRequest(Guid ClientId, DateTime Date, TransactionType Type, Guid? AssetId, int? Quantity, decimal Amount)
    {
    }
}
