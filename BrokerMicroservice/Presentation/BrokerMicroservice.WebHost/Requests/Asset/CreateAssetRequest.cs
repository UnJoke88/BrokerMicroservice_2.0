using BrokerMicroservice.Application.Models.Asset;

namespace BrokerMicroservice.WebHost.Requests.Asset
{
    public record class CreateAssetRequest(AssetType AssetType, int MinimalUnit, decimal PurchasePrice)
    {
    }
}
