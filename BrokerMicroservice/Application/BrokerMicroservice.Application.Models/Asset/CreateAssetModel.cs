using BrokerMicroservice.Application.Models.Base;


namespace BrokerMicroservice.Application.Models.Asset
{
    public record class CreateAssetModel(AssetType AssetType, int MinimalUnit, decimal PurchasePrice, Guid BrokerId) : ICreateModel;
    public enum AssetType
    {
        USD = 1,
        EUR = 2,
        RUB = 3,
        CNY = 4,
        GBP = 5,
        GOLD = 6,
        SILVER = 7,
        ALUMINUM = 8,
        OIL = 9,
        GAS = 10
    }
}
