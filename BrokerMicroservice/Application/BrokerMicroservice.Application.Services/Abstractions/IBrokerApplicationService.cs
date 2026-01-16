using BrokerMicroservice.Application.Models.Broker;


namespace BrokerMicroservice.Application.Services.Abstractions
{
    public interface IBrokerApplicationService
    {
        Task<IEnumerable<BrokerModel>> GetBrokerAsync(CancellationToken cancellationToken = default);
        Task<BrokerModel?> GetBrokerByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BrokerModel?> CreateBrokerAsync(CreateBrokerModel brokerInformation, CancellationToken cancellationToken = default);
        Task<bool> UpdateBrokerAsync(BrokerModel brokerInformation, CancellationToken cancellationToken = default);
        Task<bool> DeleteBrokerAsync(Guid id, CancellationToken cancellationToken = default);

    }
}
