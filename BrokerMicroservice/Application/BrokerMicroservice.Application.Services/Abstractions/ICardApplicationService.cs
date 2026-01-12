using BrokerMicroservice.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerMicroservice.Application.Services.Abstractions
{
    public interface ICardApplicationService<TModel, TCreateModel, in TId>
    {
        Task<TModel?> GetCardByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TModel>> GetCardsAsync(CancellationToken cancellationToken = default);
        Task<TModel?> CreateCardAsync(TCreateModel model, CancellationToken cancellationToken = default);

        //Операции с балансом карты
        Task<bool> MakeDepositAsync(TModel model, CancellationToken cancellationToken = default); 
        Task<bool> MakeSaleAsync(TModel model, CancellationToken cancellationToken = default);
        Task<bool> MakePurchaseAsync(TModel model, CancellationToken cancellationToken = default);
        Task<bool> MakeWithdrawAsync(TModel model, CancellationToken cancellationToken = default);

        Task<bool> DeleteCardAsync(TId id, CancellationToken cancellationToken = default);
    }
}
