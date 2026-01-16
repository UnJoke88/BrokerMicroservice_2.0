namespace BrokerMicroservice.WebHost.Requests.Client
{
    public record class UpdateClientRequest(string FirstName, string LastName, string? MiddleName, string Email)
    {
    }
}
