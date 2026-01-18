using BrokerMicroservice.Application.Models.Asset;
namespace BrokerMicroservice.WebHost.Requests.Asset
{
    public record class UpdateAssetRequest(Guid Id, AssetType AssetType, int MinimalUnit, decimal PurchasePrice, Guid BrokerId)
    {

    }
}
