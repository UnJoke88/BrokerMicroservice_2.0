using BrokerMicroservice.Application.Models.Base;


namespace BrokerMicroservice.Application.Models.Asset
{
    public record class AssetModel(Guid Id, AssetType AssetType, int MinimalUnit, decimal PurchasePrice, Guid BrokerId) : IModel<Guid>;
}
