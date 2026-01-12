using BrokerMicroservice.Application.Models.Base;

namespace BrokerMicroservice.Application.Models.Portfolio
{
    public record class PortfolioModel(string PortfolioNumber, decimal TotalValue) : ICreateModel;
}
