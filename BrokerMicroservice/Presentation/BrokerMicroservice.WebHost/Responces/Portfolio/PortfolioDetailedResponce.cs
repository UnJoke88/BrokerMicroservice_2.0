using BrokerMicroservice.Application.Models.Portfolio;

namespace BrokerMicroservice.WebHost.Responces.Portfolio
{
    public class PortfolioDetailedResponce(Guid Id, string PortfolioNumber, IEnumerable<PortfolioEntryModel> Entries, decimal PortfolioTotalValue)
    {
    }
}
