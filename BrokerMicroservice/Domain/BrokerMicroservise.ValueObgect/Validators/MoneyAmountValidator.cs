using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Exceptions;

namespace BrokerMicroservise.ValueObgect.Validators
{
    /// <summary>
    /// Defines a method that implements the validation of the decimal.
    /// </summary>
    public class MoneyAmountValidator : IValidator<decimal>
    {
        /// <summary>
        /// Verifies that the decimal is not negative and does not equal zero. 
        /// </summary>
        /// <param name="value">A decimal value.</param>
        /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
        /// 
        public void Validate(decimal value)
        {
            if (value < 0)
                throw new MoneyAmountNonPositiveException("Сумма не должна быть отрицательной", nameof(value), value);
            if (!IsValidAmount(value))
                throw new MoneyAmountHasMoreThanTwoDecimalPlacesException("Сумма денег имеет не более двух знаков после запятой (до сотых)", nameof(value), value);
        }

        private bool IsValidAmount(decimal value)
        {
            value = value * 100;
            value -= (int)value;
            return value == 0m;
        }
    }
}
