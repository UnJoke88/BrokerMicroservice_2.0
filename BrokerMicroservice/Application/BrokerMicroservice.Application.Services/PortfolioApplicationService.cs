using AutoMapper;
using BrokerMicroservice.Application.Models.Portfolio;
using BrokerMicroservice.Application.Models.Transaction;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.Domain.Entities;
using BrokerMicroservice.Repositories.Abstractions;


namespace BrokerMicroservice.Application.Services
{
    public class PortfolioApplicationService(IRepository<Portfolio, Guid> portfolioRepository, IMapper mapper)
         : IPortfolioApplicationService
    {
        public async Task<IEnumerable<PortfolioModel>> GetPortfolioAsync(CancellationToken cancellationToken = default)
                => (await portfolioRepository.GetAllAsync(cancellationToken = default, true))
                .Select(mapper.Map<PortfolioModel>);

        public async Task<PortfolioModel?> GetPortfolioByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var portfolio = await portfolioRepository.GetByIdAsync(id, cancellationToken);
            return portfolio is null ? null : mapper.Map<PortfolioModel>(portfolio);
        }
    }
}
