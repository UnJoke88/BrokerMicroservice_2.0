using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Exceptions;

namespace BrokerMicroservise.ValueObgect.Validators
{
    public class BrokerNameValidator : IValidator<string>
    {
        private const int MIN_LENGTH = 3;
        private const int MAX_LENGTH = 100;

        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(ExceptionMessages.BROKER_NAME_NOT_NULL_OR_WHITE_SPACE, nameof(value));

            if (value.Length < MIN_LENGTH)
                throw new BrokerNameTooShortException(value, MIN_LENGTH);

            if (value.Length > MAX_LENGTH)
                throw new BrokerNameTooLongException(value, MAX_LENGTH);
        }
    }
}
