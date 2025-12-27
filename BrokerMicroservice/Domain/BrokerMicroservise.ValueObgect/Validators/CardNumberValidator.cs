using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Exceptions;

namespace BrokerMicroservise.ValueObgect.Validators
{
    public class CardNumberValidator : IValidator<string>
    {
        public static int LENGTH => 16;

        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(ExceptionMessages.CARD_NUMBER_NOT_NULL_OR_WHITE_SPACE, nameof(value));

            if (!value.All(char.IsDigit))
                throw new CardNumberFormatException(nameof(value), value);

            if (value.Length != LENGTH)
                throw new CardNumberFormatException(nameof(value), value);
        }
    }
}
