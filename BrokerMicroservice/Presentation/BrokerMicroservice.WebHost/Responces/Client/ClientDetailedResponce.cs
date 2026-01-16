using BrokerMicroservice.Application.Models.Transaction;

namespace BrokerMicroservice.WebHost.Responces.Client
{
    public class ClientDetailedResponce(Guid Id, string FirstName, string LastName, string? MiddleName, string Email, string PhoneNumber,
        Guid CardId, Guid PortfolioId, IEnumerable<TransactionModel> Transactions, Guid BrokerId)
    {
        
    }
}
