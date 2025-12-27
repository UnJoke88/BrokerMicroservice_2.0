using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Validators;

namespace BrokerMicroservise.ValueObgect
{
    /// <summary>
    /// Номер карты клиента.
    /// </summary>
    public class CardNumber(string value)
        : ValueObject<string>(new CardNumberValidator(), value)
    {

    }
}
