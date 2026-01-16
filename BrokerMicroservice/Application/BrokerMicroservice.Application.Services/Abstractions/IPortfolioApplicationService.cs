using BrokerMicroservice.Application.Models.Portfolio;


namespace BrokerMicroservice.Application.Services.Abstractions
{
    public interface IPortfolioApplicationService
    {
        Task<IEnumerable<PortfolioModel>> GetPortfolioAsync(CancellationToken cancellationToken = default);

        Task<PortfolioModel?> GetPortfolioByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
