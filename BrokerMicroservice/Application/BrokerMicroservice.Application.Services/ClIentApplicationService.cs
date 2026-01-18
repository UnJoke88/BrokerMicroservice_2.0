using AutoMapper;
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
        IRepository<Transaction, Guid> transactionRepository, IRepository<Asset, Guid> assetRepository, IMapper mapper, ApplicationDbContext db) : IClientApplicationService //Добавляем после работы сервиса с базой напрямую (удаление списка транзакций)
                                                                                                                                                                             //ApplicationDbContext db;
    {

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
            //Проверка уникальности телефона: 1 телефон = 1 аккаунт 
            var phoneExists = await db.Set<Client>().AnyAsync(c => c.PhoneNumber == new PhoneNumber(clientInformation.PhoneNumber), cancellationToken);
            if (phoneExists) return null;

            //Проверка уникальности email: 1 email = 1 аккаунт 
            var emailExists = await db.Set<Client>().AnyAsync(c => c.Email == new Email(clientInformation.Email), cancellationToken);
            if (emailExists) return null;

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
            var client = await repository.GetByIdAsync(clientInformation.Id, cancellationToken);
            if (client is null) return false;

            // email: если меняется — проверяем уникальность
            if (client.Email.Value != clientInformation.Email)
            {
                var emailTaken = await db.Set<Client>()
                    .AnyAsync(c => c.Id != client.Id && c.Email == new Email(clientInformation.Email), cancellationToken);

                if (emailTaken) return false;
            }

            var changed = false;

            if (client.FirstName.Value != clientInformation.FirstName)
                changed |= client.ChangeUsername(new(clientInformation.FirstName));

            if (client.LastName.Value != clientInformation.LastName)
                changed |= client.ChangeLastName(new(clientInformation.LastName));

            var middle = clientInformation.MiddleName;
            if ((client.MiddleName?.Value) != middle)
                changed |= client.ChangeMiddleName(middle is null ? null : new(middle));

            if (client.Email.Value != clientInformation.Email)
                changed |= client.ChangeEmail(new(clientInformation.Email));

            if (!changed) return true; // ничего не меняли - успех

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

            var card = await cardRepository.GetByIdAsync(client.CardId, cancellationToken);
            if (card is null)
                return null;

            var portfolio = await portfolioRepository.GetByIdAsync(client.PortfolioId, cancellationToken);
            if (portfolio is null)
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

            var transactions = await db.Set<Transaction>().Where(t => t.Client.Id == client.Id).ToListAsync(cancellationToken);
            if (transactions is null)
                return false;

            db.Set<Transaction>().RemoveRange(transactions);
             
            //Удаление
            db.Remove(card);
            db.Remove(portfolio);
            db.Remove(client);
            await db.SaveChangesAsync(cancellationToken);
            return true;

        }
    }
}
