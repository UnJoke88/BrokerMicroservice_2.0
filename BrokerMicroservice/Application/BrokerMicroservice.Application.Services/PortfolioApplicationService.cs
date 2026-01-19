using AutoMapper;
using BrokerMicroservice.Application.Models.Portfolio;
using BrokerMicroservice.Application.Models.Transaction;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.Domain.Entities;
using BrokerMicroservice.Infrastructure.EntityFramework;
using BrokerMicroservice.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;


namespace BrokerMicroservice.Application.Services
{
    public class PortfolioApplicationService(IRepository<Portfolio, Guid> portfolioRepository,ApplicationDbContext db, IMapper mapper) : IPortfolioApplicationService
    {
        public async Task<IEnumerable<PortfolioModel>> GetPortfolioAsync(
     CancellationToken cancellationToken = default)
        {
            // 1) Берём портфели
            var portfolios = await portfolioRepository
                .GetAllAsync(cancellationToken, true);

            var portfolioModels = portfolios
                .Select(mapper.Map<PortfolioModel>)
                .ToList();

            // 2) Берём ВСЕ entries + Asset
            var entries = await db.Set<PortfolioEntry>()
                .AsNoTracking()
                .Include(e => e.Asset)
                .ToListAsync(cancellationToken);

            // 3) Группируем В ПАМЯТИ
            var totals = entries
                .GroupBy(e => e.PortfolioId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(e => e.Asset.PurchasePrice.Value * e.Quantity.Value)
                );

            // 4) Проставляем total
            foreach (var model in portfolioModels)
            {
                model.PortfolioTotalValue =
                    totals.TryGetValue(model.Id, out var total)
                        ? total
                        : 0;
            }

            return portfolioModels;
        }

        public async Task<PortfolioModel?> GetPortfolioByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // 1) Берём сам портфель
            var portfolio = await portfolioRepository.GetByIdAsync(id, cancellationToken);
            if (portfolio is null)
                return null;

            // 2) Берём все entries этого портфеля + Asset (важно!)
            var entries = await db.Set<PortfolioEntry>()
                .AsNoTracking()
                .Where(e => e.PortfolioId == id)
                .Include(e => e.Asset)
                .ToListAsync(cancellationToken);

            // 3) Маппим портфель
            var model = mapper.Map<PortfolioModel>(portfolio);

            // 4) Если в PortfolioModel есть Entries и они settable (get; set;)
            model.Entries = entries.Select(mapper.Map<PortfolioEntryModel>).ToList();

            // 5) (если домен видит пусто)
            // Ниже универсально считаем руками.
            model.PortfolioTotalValue = entries.Sum(e =>
                e.Asset.PurchasePrice.Value * e.Quantity.Value
            );

            return model;
        }
    }
}
