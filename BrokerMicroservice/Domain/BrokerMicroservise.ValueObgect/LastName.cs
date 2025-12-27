using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Validators;

namespace BrokerMicroservise.ValueObgect
{
    /// <summary>
    /// Представляет фамилию клиента.
    /// </summary>
    public class LastName(string value)
        : ValueObject<string>(new LastNameValidator(), value)
    {

    }
}
