using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Validators;

namespace BrokerMicroservise.ValueObgect
{
    /// <summary>
    /// Номер телефона клиента.
    /// </summary>
    public class PhoneNumber(string value)
        : ValueObject<string>(new PhoneNumberValidator(), value)
    {

    }
}
