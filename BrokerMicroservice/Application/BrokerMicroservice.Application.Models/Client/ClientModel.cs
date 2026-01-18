using BrokerMicroservice.Application.Models.Base;
using BrokerMicroservice.Application.Models.Transaction;


namespace BrokerMicroservice.Application.Models.Client
{
    public sealed record class ClientModel : IModel<Guid>
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string? MiddleName { get; init; }
        public string Email { get; init; } = null!;
        public string PhoneNumber { get; init; } = null!;

        public  Guid CardId { get; init; }

        public Guid PortfolioId { get; init; }

        public IEnumerable<TransactionModel> Transactions { get; init; } 
        public Guid BrokerId { get; init; }
    }
}
