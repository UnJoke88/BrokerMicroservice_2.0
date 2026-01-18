namespace BrokerMicroservice.WebHost.Requests.Client
{
    public record class UpdateClientRequest(Guid Id, string FirstName, string LastName, string? MiddleName, string Email)
    {
    }
}
