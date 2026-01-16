using AutoMapper;
using BrokerMicroservice.Application.Models.Broker;
using BrokerMicroservice.Application.Models.Card;
using BrokerMicroservice.Application.Models.Client;
using BrokerMicroservice.Application.Models.Portfolio;
using BrokerMicroservice.Application.Models.Transaction;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.Domain.Entities;
using BrokerMicroservice.Repositories.Abstractions;
using BrokerMicroservise.ValueObgect;
using BrokerMicroservice.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;


namespace BrokerMicroservice.Application.Services
{
    public class ClientApplicationService(IRepository<Client, Guid> repository, IRepository<Broker, Guid> brokerRepository,
        IRepository<Card, Guid> cardRepository, IRepository<Portfolio, Guid> portfolioRepository,
        IRepository<Transaction, Guid> transactionRepository, IRepository<Asset, Guid> assetRepository, IMapper mapper) : IClientApplicationService
    {
        //Добавляем после работы сервиса с базой напрямую (удаление списка транзакций)
        private readonly ApplicationDbContext db;

        public async Task<IEnumerable<ClientModel>> GetClientAsync(CancellationToken cancellationToken = default)
            => (await repository.GetAllAsync(cancellationToken = default, true))
            .Select(mapper.Map<ClientModel>);

        public async Task<ClientModel?> GetClientByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var client = await repository.GetByIdAsync(id, cancellationToken);
            return client is null ? null : mapper.Map<ClientModel>(client);
        }

        public async Task<ClientModel?> CreateClientAsync(CreateClientModel clientInformation, CancellationToken cancellationToken = default)
        {
            var broker = await brokerRepository.GetByIdAsync(clientInformation.BrokerId,cancellationToken);
            if (broker == null) return null;

            var client = broker.CreateClient(new LastName(clientInformation.LastName), new FirstName(clientInformation.FirstName),
                clientInformation.MiddleName is null ? null : new(clientInformation.MiddleName), new PhoneNumber(clientInformation.PhoneNumber),
                new Email(clientInformation.Email));
            if (client == null) return null;

            //Сохраняем в БД
            var createdCard = await cardRepository.AddAsync(client.Card, cancellationToken);
            if (createdCard == null) return null;
            var createdPortfolio = await portfolioRepository.AddAsync(client.Portfolio, cancellationToken);
            if (createdPortfolio == null) return null;
            var createdClient = await repository.AddAsync(client, cancellationToken);
            var updateBroker = await brokerRepository.UpdateAsync(broker, cancellationToken);

            mapper.Map<CardModel>(createdCard);
            mapper.Map<PortfolioModel>(createdPortfolio);
            return createdClient is null ? null : mapper.Map<ClientModel>(createdClient);

        }

        public async Task<bool> UpdateClientAsync(ClientModel clientInformation, CancellationToken cancellationToken = default)
        {
            var clientById = repository.GetByIdAsync(clientInformation.Id, cancellationToken);
            if (clientById.Result is null)
                return false;

            var client = clientById.Result;

            var okFirstName = client.ChangeUsername(new(clientInformation.FirstName));
            var okLastName = client.ChangeLastName(new(clientInformation.LastName));
            var okMiddleName = client.ChangeMiddleName(clientInformation.MiddleName is null ? null : new(clientInformation.MiddleName));
            var okEmail = client.ChangeEmail(new(clientInformation.Email));

            if (!okFirstName || !okLastName || !okMiddleName || !okEmail)
                return false;

            client = mapper.Map<Client>(clientInformation);
            return await repository.UpdateAsync(client, cancellationToken);
        }


        public async Task<TransactionModel?> BuyAssetAsync(CreateTransactionModel transactionInformation, CancellationToken cancellationToken = default)
        {
            var client = await repository.GetByIdAsync(transactionInformation.ClientId, cancellationToken);
            if (client is null)
                return null;

            if (transactionInformation.AssetId == null) return null;
            var asset = await assetRepository.GetByIdAsync(transactionInformation.AssetId.Value, cancellationToken);
            if (asset is null)
                return null;

            if (transactionInformation.Quantity == null) return null;
            var transaction = client.BuyAsset(asset,new(transactionInformation.Quantity.Value));

            var updadedCard = await cardRepository.UpdateAsync(client.Card, cancellationToken);
            var updadedPortfolio = await portfolioRepository.UpdateAsync(client.Portfolio, cancellationToken);
            var createdTransaction = await transactionRepository.AddAsync(transaction, cancellationToken);

            mapper.Map<CardModel>(updadedCard);
            mapper.Map<PortfolioModel>(updadedPortfolio);
            return createdTransaction is null ? null : mapper.Map<TransactionModel>(createdTransaction);
        }

        public async Task<TransactionModel?> MakeSaleAsync(CreateTransactionModel transactionInformation, CancellationToken cancellationToken = default)
        {
            var client = await repository.GetByIdAsync(transactionInformation.ClientId, cancellationToken);
            if (client is null)
                return null;

            if (transactionInformation.AssetId == null) return null;
            var asset = await assetRepository.GetByIdAsync(transactionInformation.AssetId.Value, cancellationToken);
            if (asset is null)
                return null;

            if (transactionInformation.Quantity == null) return null;
            var transaction = client.MakeSale(asset, new(transactionInformation.Quantity.Value));

            var updadedCard = await cardRepository.UpdateAsync(client.Card, cancellationToken);
            var updadedPortfolio = await portfolioRepository.UpdateAsync(client.Portfolio, cancellationToken);
            var createdTransaction = await transactionRepository.AddAsync(transaction, cancellationToken);

            mapper.Map<CardModel>(updadedCard);
            mapper.Map<PortfolioModel>(updadedPortfolio);
            return createdTransaction is null ? null : mapper.Map<TransactionModel>(createdTransaction);
        }

        public async Task<TransactionModel?> MakeDepositAsync(CreateTransactionModel transactionInformation, CancellationToken cancellationToken = default)
        {
            var client = await repository.GetByIdAsync(transactionInformation.ClientId, cancellationToken);
            if (client is null)
                return null;
           
            var transaction = client.MakeDeposit(new(transactionInformation.Amount));
            var updadedCard = await cardRepository.UpdateAsync(client.Card, cancellationToken);
            var createdTransaction = await transactionRepository.AddAsync(transaction, cancellationToken);
            mapper.Map<CardModel>(updadedCard);
            return createdTransaction is null ? null : mapper.Map<TransactionModel>(createdTransaction);
        }

        public async Task<TransactionModel?> MakeWithdrawAsync(CreateTransactionModel transactionInformation, CancellationToken cancellationToken = default)
        {
            var client = await repository.GetByIdAsync(transactionInformation.ClientId, cancellationToken);
            if (client is null)
                return null;

            var transaction = client.MakeWithdraw(new(transactionInformation.Amount));
            var updadedCard = await cardRepository.UpdateAsync(client.Card, cancellationToken);
            var createdTransaction = await transactionRepository.AddAsync(transaction, cancellationToken);
            mapper.Map<CardModel>(updadedCard);
            return createdTransaction is null ? null : mapper.Map<TransactionModel>(createdTransaction);
        }

        public async Task<bool> DeleteClientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var client = await repository.GetByIdAsync(id, cancellationToken);
            if (client is null)
                return false;

            var card = await cardRepository.GetByIdAsync(client.CardId, cancellationToken);
            if (card is null)
                return false;

            var portfolio = await portfolioRepository.GetByIdAsync(client.PortfolioId, cancellationToken);
            if (portfolio is null)
                return false;

            var broker = await brokerRepository.GetByIdAsync(client.BrokerId, cancellationToken);
            if (broker is null)
                return false;

            //Гарантированно удаляет все транзакции клиента 
            var transactions = await db.Set<Transaction>().Where(t => t.Client.Id == client.Id).ToListAsync(cancellationToken);
            db.Set<Transaction>().RemoveRange(transactions);
            await db.SaveChangesAsync(cancellationToken);

            broker.DeleteClient(client);
            var updateBroker = await brokerRepository.UpdateAsync(broker, cancellationToken);
            var deleteCard = await cardRepository.DeleteAsync(card, cancellationToken);
            var deletePortfolio = await portfolioRepository.DeleteAsync(portfolio, cancellationToken);
            if (deleteCard == false) return false;
            if (deletePortfolio == false) return false;
            return client is null ? false : await repository.DeleteAsync(client, cancellationToken);

        }
    }
}
