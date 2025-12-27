using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Exceptions;

namespace BrokerMicroservise.ValueObgect.Validators
{
    public class LastNameValidator : IValidator<string>
    {
        public static int MAX_LENGTH => 55;
        public static int MIN_LENGTH => 2;

        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(ExceptionMessages.LASTNAME_NOT_NULL_OR_WHITE_SPACE, nameof(value));

            if (value.Length > MAX_LENGTH)
                throw new LastNameTooLongException(value, MAX_LENGTH);

            if (value.Length < MIN_LENGTH)
                throw new LastNameTooShortException(value, MIN_LENGTH);
        }
    }
}
