using BrokerMicroservice.Application.Models.Asset;
using BrokerMicroservice.Application.Models.Base;
using BrokerMicroservice.Application.Models.Client;


namespace BrokerMicroservice.Application.Models.Broker
{
    public sealed record class BrokerModel : IModel<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ClientModel> Clients { get; init; }
        public IEnumerable<AssetModel> Assets { get; init; }
    }
}
