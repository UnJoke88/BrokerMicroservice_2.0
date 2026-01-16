namespace BrokerMicroservice.WebHost.Requests.Client
{
    public record class CreateClientRequest(string FirstName, string LastName, string? MiddleName, string Email, string PhoneNumber, Guid BrokerId)
    {
    }
}
