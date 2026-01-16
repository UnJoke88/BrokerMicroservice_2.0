using BrokerMicroservice.Application.Models.Asset;
using BrokerMicroservice.Application.Models.Client;

namespace BrokerMicroservice.WebHost.Responces.Broker
{
    public record class BrokerDetailedResponce(Guid Id, string Name, IEnumerable<ClientModel> Clients, IEnumerable<AssetModel> Assets)
    {

    }
}
