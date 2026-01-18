using BrokerMicroservice.Application.Models.Asset;


namespace BrokerMicroservice.WebHost.Responses.Portfolio
{
    public class PortfolioEntryResponse
    {
        public Guid Id { get; init; }
        public Guid AssetId { get; init; }
        public AssetType AssetType { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal TotalValue { get; init; }
    }
}