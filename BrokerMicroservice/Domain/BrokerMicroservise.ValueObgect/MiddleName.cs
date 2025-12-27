using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Validators;

namespace BrokerMicroservise.ValueObgect
{
    /// <summary>
    /// Представляет отчество клиента (может отсутствовать).
    /// </summary>
    public class MiddleName(string? value)
        : ValueObject<string?>(new MiddleNameValidator(), value)
    {

    }
}
