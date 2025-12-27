using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Exceptions;

namespace BrokerMicroservise.ValueObgect.Validators
{
    public class PortfolioNumberValidator : IValidator<string>
    {
        public static int LENGTH => 8;

        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(ExceptionMessages.PORTFOLIO_NUMBER_NOT_NULL_OR_WHITE_SPACE, nameof(value));

            if (!value.All(char.IsDigit))
                throw new PortfolioNumberFormatException(nameof(value), value);

            if (value.Length != LENGTH)
                throw new PortfolioNumberLengthException(value, LENGTH);
        }
    }
}
