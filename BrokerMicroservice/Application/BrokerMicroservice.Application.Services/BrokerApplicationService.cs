using AutoMapper;
using BrokerMicroservice.Application.Models.Broker;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.Domain.Entities;
using BrokerMicroservice.Repositories.Abstractions;
using BrokerMicroservise.ValueObgect;


namespace BrokerMicroservice.Application.Services
{
    public class BrokerApplicationService(IRepository<Broker, Guid> repository, IMapper mapper) : IBrokerApplicationService
    {
        public async Task<IEnumerable<BrokerModel>> GetBrokerAsync(CancellationToken cancellationToken = default)
            => (await repository.GetAllAsync(cancellationToken = default, true))
            .Select(mapper.Map<BrokerModel>);

        public async Task<BrokerModel?> GetBrokerByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var broker = await repository.GetByIdAsync(id, cancellationToken);
            return broker is null ? null : mapper.Map<BrokerModel>(broker);
        }

        public async Task<BrokerModel?> CreateBrokerAsync(CreateBrokerModel brokerInformation, CancellationToken cancellationToken = default)
        {
            Broker broker = new(new BrokerName(brokerInformation.Name));
            var createdBroker = await repository.AddAsync(broker, cancellationToken);
            return createdBroker is null ? null : mapper.Map<BrokerModel>(createdBroker);
        }

        public async Task<bool> UpdateBrokerAsync(BrokerModel brokerInformation, CancellationToken ct = default)
        {
            var broker = await repository.GetByIdAsync(brokerInformation.Id, ct);
            if (broker is null) return false;

            var changed = broker.ChangeBrokerName(new BrokerName(brokerInformation.Name));
            if (!changed) return true;

            return await repository.UpdateAsync(broker, ct);
        }

        public async Task<bool> DeleteBrokerAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var broker = await repository.GetByIdAsync(id, cancellationToken);
            return broker is null ? false : await repository.DeleteAsync(broker, cancellationToken);
        }
    }
}
