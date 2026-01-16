using BrokerMicroservice.Application.Models.Asset;

namespace BrokerMicroservice.WebHost.Responces.Asset
{
    public record class AssetShortResponce(Guid Id, AssetType AssetType)
    {
    }
}
