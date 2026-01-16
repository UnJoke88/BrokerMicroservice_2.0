using BrokerMicroservice.Application.Models.Base;


namespace BrokerMicroservice.Application.Models.Client
{
    public record class CreateClientModel(string FirstName, string LastName, string? MiddleName, string Email, string PhoneNumber, Guid BrokerId) : ICreateModel; 
    
}
