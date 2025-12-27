using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Validators;

namespace BrokerMicroservise.ValueObgect
{
    /// <summary>
    /// Название брокерской компании.
    /// </summary>
    public class BrokerName(string value)
        : ValueObject<string>(new BrokerNameValidator(), value)
    {

    }
}
