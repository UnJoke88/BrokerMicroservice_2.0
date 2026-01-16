using BrokerMicroservice.Application.Models.Transaction;


namespace BrokerMicroservice.Application.Services.Abstractions
{
    public interface ITransactionApplicationService
    {
        Task<IEnumerable<TransactionModel>> GetTransactionsAsync(CancellationToken cancellationToken = default);

        Task<TransactionModel?> GetTransactionByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
