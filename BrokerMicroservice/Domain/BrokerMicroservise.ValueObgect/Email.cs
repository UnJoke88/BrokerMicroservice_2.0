using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Validators;

namespace BrokerMicroservise.ValueObgect
{
    /// <summary>
    /// Email-адрес клиента.
    /// </summary>
    public class Email(string value)
        : ValueObject<string>(new EmailValidator(), value)
    {

    }
}
