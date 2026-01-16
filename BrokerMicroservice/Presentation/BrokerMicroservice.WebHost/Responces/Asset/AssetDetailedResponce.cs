using BrokerMicroservice.Application.Models.Asset;

namespace BrokerMicroservice.WebHost.Responces.Asset
{
    public record class AssetDetailedResponce(Guid Id, AssetType AssetType, int MinimalUnit, decimal PurchasePrice, Guid BrokerId)
    {
    }
}
