using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Validators;

namespace BrokerMicroservise.ValueObgect
{
    /// <summary>
    /// Минимальная единица при покупке актива за одну операцию.
    /// </summary>
    public class MinimalUnit(int value)
        : ValueObject<int>(new MinimalUnitValidator(), value)
    {
        // Оператор сложения для двух объектов типа MinimalUnit
        public static MinimalUnit operator +(MinimalUnit m1, MinimalUnit m2)
            => new(m1.Value + m2.Value);

        // Оператор вычитания для двух объектов типа MinimalUnit
        public static MinimalUnit operator -(MinimalUnit m1, MinimalUnit m2)
            => new(m1.Value - m2.Value);

        // Оператор больше(>) для двух объектов типа MinimalUnit
        public static bool operator >(MinimalUnit m1, MinimalUnit m2)
            => m1.Value > m2.Value;

        // Оператор меньше(<) для двух объектов типа MinimalUnit
        public static bool operator <(MinimalUnit m1, MinimalUnit m2)
            => m1.Value < m2.Value;

        // Оператор больше или равно (>=) для двух объектов типа MinimalUnit
        public static bool operator >=(MinimalUnit m1, MinimalUnit m2)
            => m1.Value >= m2.Value;

        // Оператор меньше или равно (<=) для двух объектов типа MinimalUnit
        public static bool operator <=(MinimalUnit m1, MinimalUnit m2)
            => m1.Value <= m2.Value;

        // Оператор меньше или равно (==) для двух объектов типа MinimalUnit
        public static bool operator ==(MinimalUnit m1, MinimalUnit m2)
            => m1.Value == m2.Value;

        // Оператор меньше или равно (!=) для двух объектов типа MinimalUnit
        public static bool operator !=(MinimalUnit m1, MinimalUnit m2)
            => m1.Value != m2.Value;

        // Оператор больше(>) для int и MinimalUnit
        public static bool operator >(MinimalUnit m1, int m2)
            => m1.Value > m2;

        // Оператор меньше(<) для int и MinimalUnit
        public static bool operator <(MinimalUnit m1, int m2)
            => m1.Value < m2;

        // Оператор больше или равно (>=) для int и MinimalUnit
        public static bool operator >=(MinimalUnit m1, int m2)
            => m1.Value >= m2;

        // Оператор меньше или равно (<=) для int и MinimalUnit
        public static bool operator <=(MinimalUnit m1, int m2)
            => m1.Value <= m2;

        // Оператор меньше или равно (==) для int и MinimalUnit
        public static bool operator ==(MinimalUnit m1, int m2)
            => m1.Value == m2;

        // Оператор меньше или равно (!=) для int и MinimalUnit
        public static bool operator !=(MinimalUnit m1, int m2)
            => m1.Value != m2;
    }
}
