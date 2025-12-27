using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Exceptions;

namespace BrokerMicroservise.ValueObgect.Validators
{
    public class PhoneNumberValidator : IValidator<string>
    {
        public static int REQUIRED_LENGTH => 11;

        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(ExceptionMessages.PHONENUMBER_NOT_NULL_OR_WHITE_SPACE, nameof(value));

            if (!value.All(char.IsDigit))
                throw new PhoneNumberFormatException(value);
        }
    }
}
