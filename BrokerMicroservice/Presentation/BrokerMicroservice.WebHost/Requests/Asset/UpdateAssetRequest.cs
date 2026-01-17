namespace BrokerMicroservice.WebHost.Requests.Asset
{
    public record class UpdateAssetRequest(Guid Id, int MinimalUnit, decimal PurchasePrice)
    {
    }
}
