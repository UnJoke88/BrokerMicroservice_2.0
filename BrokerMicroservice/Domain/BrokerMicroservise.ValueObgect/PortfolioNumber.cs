using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Validators;

namespace BrokerMicroservise.ValueObgect
{
    /// <summary>
    /// Номер карты клиента.
    /// </summary>
    public class PortfolioNumber(string value)
        : ValueObject<string>(new PortfolioNumberValidator(), value)
    {

    }
}
