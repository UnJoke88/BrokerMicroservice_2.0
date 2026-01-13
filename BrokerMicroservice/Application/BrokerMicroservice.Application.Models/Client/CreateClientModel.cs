using BrokerMicroservice.Application.Models.Base;


namespace BrokerMicroservice.Application.Models.Client
{
    public record class CreateClientModel(string FirstName, string LastName, string? MiddleName, string Email, string PhoneNumber) : ICreateModel; //Данные для создания пользователя.
    //Продумать как на созданный акк добавить CardId, PortfolioId, BrokeId, transations
}
