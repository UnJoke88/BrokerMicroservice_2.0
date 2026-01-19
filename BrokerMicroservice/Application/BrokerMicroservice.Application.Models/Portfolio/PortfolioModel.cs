using BrokerMicroservice.Application.Models.Base;
using BrokerMicroservise.ValueObgect;
using System;


namespace BrokerMicroservice.Application.Models.Portfolio
{
    public sealed record class PortfolioModel : IModel<Guid>
    {
        public Guid Id { get; init; }

        public string PortfolioNumber { get; init; } = null!;

        public IEnumerable<PortfolioEntryModel> Entries { get; set; } 
            = Array.Empty<PortfolioEntryModel>(); //задаёт Entries пустой коллекцией по умолчанию, чтобы она не была null

        public decimal PortfolioTotalValue { get; set; }
 
    }
}
