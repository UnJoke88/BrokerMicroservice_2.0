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

        

    }
}
