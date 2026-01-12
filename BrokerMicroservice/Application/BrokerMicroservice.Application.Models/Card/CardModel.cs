using System;
using BrokerMicroservice.Application.Models.Base;

namespace BrokerMicroservice.Application.Models.Card
{
    ///<summary>
    ///DTO для получения (чтения) карты.
    ///</summary>
    public record class CardModel(Guid Id, string CardNumber, decimal CashBalance) : IModel<Guid>;
}

