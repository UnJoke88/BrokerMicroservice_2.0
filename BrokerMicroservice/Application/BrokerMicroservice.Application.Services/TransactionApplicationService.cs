using AutoMapper;
using BrokerMicroservice.Application.Models.Client;
using BrokerMicroservice.Application.Models.Transaction;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.Domain.Entities;
using BrokerMicroservice.Repositories.Abstractions;


namespace BrokerMicroservice.Application.Services
{
    public class TransactionApplicationService(IRepository<Transaction, Guid> transactionRepository, IMapper mapper)
         : ITransactionApplicationService
    {
        public async Task<IEnumerable<TransactionModel>> GetTransactionsAsync(CancellationToken cancellationToken = default)
                => (await transactionRepository.GetAllAsync(cancellationToken = default, true))
                .Select(mapper.Map<TransactionModel>);

        public async Task<TransactionModel?> GetTransactionByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var transaction = await transactionRepository.GetByIdAsync(id, cancellationToken);
            return transaction is null ? null : mapper.Map<TransactionModel>(transaction);
        }
    }
}
