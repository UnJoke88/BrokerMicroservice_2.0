using BrokerMicroservice.Application.Models.Base;
using BrokerMicroservice.Application.Models.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerMicroservice.Application.Services.Abstractions
{
    public interface ICardApplicationService
    {
        Task<CardModel?> GetCardByIdAsync(Guid Id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CardModel>> GetCardsAsync(CancellationToken cancellationToken = default);
        
    }
}
