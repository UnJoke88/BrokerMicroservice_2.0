using AutoMapper;
using BrokerMicroservice.Application.Models.Base;
using BrokerMicroservice.Application.Models.Card;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.Domain.Entities;
using BrokerMicroservice.Repositories.Abstractions;
using System.Threading;

namespace BrokerMicroservice.Application.Services
{
    public class CardApplicationService(
         IRepository<Card, Guid> cardRepository, IRepository<Broker, Guid> brokerRepository,IMapper mapper) 
        : ICardApplicationService<CardModel, CreateCardModel, Guid>
    {
        //Получение модели по id
        public async Task<CardModel?> GetCardByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var card = await cardRepository.GetByIdAsync(id, cancellationToken);
            return card is null ? null : mapper.Map<CardModel>(card);
        }

        //Получение всех карт (моделей)
        public async Task<IEnumerable<CardModel>> GetCardsAsync(CancellationToken cancellationToken = default)
         => (await cardRepository.GetAllAsync(cancellationToken, true))
            .Select(mapper.Map<CardModel>);

        // Создание здесь тарифной зоны
        Task<CardModel?> CreateCardAsync(CreateCardModel createCardmodel, CancellationToken cancellationToken = default)
        {
            // Администратор, создающий модель
            var broker = await brokerRepository.GetByIdAsync(createCardmodel.BrokerId, cancellationToken);
            if (broker is null)
                return null;

            var CreateCard = Broker.CreateCard(
            new(createCardmodel.TarifZoneName), // можно просто написать в коде new(..) без точного типа
            new(createCardmodel.Price),S
            new(createCardmodel.Distance));

            if ( is null)
                return null;

            // Добавление тарифной зоны
            var createdTariffZone = await tariffZoneRepository.AddAsync(tariffZone, cancellationToken);
            return createdTariffZone is null ? null : mapper.Map<TariffZoneModel>(createdTariffZone);
        }

    }
}
