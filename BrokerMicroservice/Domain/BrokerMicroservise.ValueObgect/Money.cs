using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Validators;

namespace BrokerMicroservise.ValueObgect
{
    /// <summary>
    ///  Деньги.
    /// </summary>
    /// <param name="amount">The amount of the money.</param>
    public class Money(decimal amountInRub) : ValueObject<decimal>(new MoneyAmountValidator(),
        Math.Round(amountInRub, 2, MidpointRounding.AwayFromZero))
    {
        public static Money operator +(Money m1, Money m2)
            => new(m1.Value + m2.Value);

        public static Money operator -(Money m1, Money m2)
            => new(m1.Value - m2.Value);

        public static Money operator *(Money m1, Quantity m2)
            => new(m1.Value * m2.Value);

        public static bool operator >(Money m1, Money m2)
            => m1.Value > m2.Value;

        public static bool operator <(Money m1, Money m2)
            => m1.Value < m2.Value;

        public static bool operator >=(Money m1, Money m2)
            => m1.Value >= m2.Value;

        public static bool operator <=(Money m1, Money m2)
            => m1.Value <= m2.Value;
    }
}
