namespace BrokerMicroservice.WebHost.Requests.Asset
{
    public record class UpdateAssetRequest(int MinimalUnit, decimal PurchasePrice)
    {
    }
}
