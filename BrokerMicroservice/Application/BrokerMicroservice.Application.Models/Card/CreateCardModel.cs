using BrokerMicroservice.Application.Models.Base;

namespace BrokerMicroservice.Application.Models.Card
{
    /// <summary>
    /// DTO для создания карты.
    /// В домене Card создаётся из CardNumber, а CashBalance выставляется внутри домена (обычно 0),
    /// поэтому здесь только номер карты.
    /// </summary>
    public record class CreateCardModel() : ICreateModel;
}

