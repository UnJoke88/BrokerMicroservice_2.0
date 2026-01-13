using BrokerMicroservice.Application.Models.Base;

namespace BrokerMicroservice.Application.Models.Portfolio
{
    public record class CreatePortfolioModel(string PortfolioNumber, decimal TotalValue) : ICreateModel;
}
