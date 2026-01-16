

using BrokerMicroservice.Application.Models.Client;
using BrokerMicroservice.Application.Models.Transaction;
using BrokerMicroservice.Domain.Entities;

namespace BrokerMicroservice.Application.Services.Abstractions
{
    public interface IClientApplicationService
    {
        Task<IEnumerable<ClientModel>> GetClientAsync(CancellationToken cancellationToken = default);
        Task<ClientModel?> GetClientByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ClientModel?> CreateClientAsync(CreateClientModel clientInformation, CancellationToken cancellationToken = default);
        Task<bool> UpdateClientAsync(ClientModel clientInformation, CancellationToken cancellationToken = default);

        //Операции с балансом карты
        Task<TransactionModel?> BuyAssetAsync(CreateTransactionModel transactionInformation, CancellationToken cancellationToken = default);
        Task<TransactionModel?> MakeSaleAsync(CreateTransactionModel transactionInformation, CancellationToken cancellationToken = default);
        Task<TransactionModel?> MakeDepositAsync(CreateTransactionModel transactionInformation, CancellationToken cancellationToken = default);
        Task<TransactionModel?> MakeWithdrawAsync(CreateTransactionModel transactionInformation, CancellationToken cancellationToken = default);

        Task<bool> DeleteClientAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
