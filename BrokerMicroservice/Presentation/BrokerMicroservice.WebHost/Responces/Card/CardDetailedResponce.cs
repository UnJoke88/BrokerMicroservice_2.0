namespace BrokerMicroservice.WebHost.Responces.Card
{
    public record class CardDetailedResponce(Guid Id, string CardNumber, decimal CashBalance)
    {
    }
}
