using BrokerMicroservice.Application.Models.Base;


namespace BrokerMicroservice.Application.Models.Broker
{
    public record class CreateBrokerModel(string Name) : ICreateModel;
}
