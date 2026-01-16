using BrokerMicroservice.Application.Models.Base;
using BrokerMicroservice.Application.Models.Transaction;


namespace BrokerMicroservice.Application.Models.Client
{
    public sealed record class ClientModel : IModel<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; }

        public  Guid CardId { get; }

        public Guid PortfolioId { get; }

        public IEnumerable<TransactionModel> Transactions { get; init; }
        public Guid BrokerId { get; set; }
    }
}
