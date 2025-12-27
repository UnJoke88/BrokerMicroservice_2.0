using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Exceptions;

namespace BrokerMicroservise.ValueObgect.Validators
{
    public class MiddleNameValidator : IValidator<string?>
    {
        public static int MAX_LENGTH => 55;

        public void Validate(string? value)
        {
            if (value is null)
                return;

            if (value.Length > MAX_LENGTH)
                throw new MiddleNameTooLongException(value, MAX_LENGTH);
        }
    }
}
