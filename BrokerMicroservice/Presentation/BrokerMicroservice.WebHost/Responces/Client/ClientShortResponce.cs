namespace BrokerMicroservice.WebHost.Responces.Client
{
    public record class ClientShortResponce(Guid Id, string FirstName, string LastName, string? MiddleName, Guid BrokerId)
    {
    }
}
