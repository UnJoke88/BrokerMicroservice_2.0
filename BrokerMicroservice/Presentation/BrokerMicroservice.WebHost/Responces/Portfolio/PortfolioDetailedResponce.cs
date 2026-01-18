using BrokerMicroservice.WebHost.Responses.Portfolio;

namespace BrokerMicroservice.WebHost.Responces.Portfolio
{
    public class PortfolioDetailedResponce
    {
        public Guid Id { get; init; }

        public string PortfolioNumber { get; init; } = null!;

        public IEnumerable<PortfolioEntryResponse> Entries { get; init; }
            = Array.Empty<PortfolioEntryResponse>(); //задаёт Entries пустой коллекцией по умолчанию, чтобы она не была null

        public decimal PortfolioTotalValue { get; init; }
    }
}
