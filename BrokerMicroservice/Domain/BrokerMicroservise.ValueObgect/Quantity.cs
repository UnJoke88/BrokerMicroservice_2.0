using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Validators;

namespace BrokerMicroservise.ValueObgect
{
    /// <summary>
    /// Единица для передачи кол-ва актива (В портфеле может быть 0 активов)
    /// </summary>
    public class Quantity(int value)
        : ValueObject<int>(new QuantityValidator(), value)
    {
        // Оператор сложения для двух объектов типа Quantity
        public static Quantity operator +(Quantity q1, Quantity q2)
            => new(q1.Value + q2.Value);

        // Оператор вычитания для двух объектов типа Quantity
        public static Quantity operator -(Quantity q1, Quantity q2)
            => new(q1.Value - q2.Value);

        // Оператор больше (>) для двух объектов типа Quantity
        public static bool operator >(Quantity q1, Quantity q2)
            => q1.Value > q2.Value;

        // Оператор меньше (<) для двух объектов типа Quantity
        public static bool operator <(Quantity q1, Quantity q2)
            => q1.Value < q2.Value;

        // Оператор больше или равно (>=) для двух объектов типа Quantity
        public static bool operator >=(Quantity q1, Quantity q2)
            => q1.Value >= q2.Value;

        // Оператор меньше или равно (<=) для двух объектов типа Quantity
        public static bool operator <=(Quantity q1, Quantity q2)
            => q1.Value <= q2.Value;

        // Оператор равенства (==) для двух объектов типа Quantity
        public static bool operator ==(Quantity q1, Quantity q2)
            => q1.Value == q2.Value;

        // Оператор неравенства (!=) для двух объектов типа Quantity
        public static bool operator !=(Quantity q1, Quantity q2)
            => q1.Value != q2.Value;

        // Оператор больше (>) для int и Quantity
        public static bool operator >(Quantity q1, int q2)
            => q1.Value > q2;

        // Оператор меньше (<) для int и Quantity
        public static bool operator <(Quantity q1, int q2)
            => q1.Value < q2;

        // Оператор больше или равно (>=) для int и Quantity
        public static bool operator >=(Quantity q1, int q2)
            => q1.Value >= q2;

        // Оператор меньше или равно (<=) для int и Quantity
        public static bool operator <=(Quantity q1, int q2)
            => q1.Value <= q2;

        // Оператор равенства (==) для int и Quantity
        public static bool operator ==(Quantity q1, int q2)
            => q1.Value == q2;

        // Оператор неравенства (!=) для int и Quantity
        public static bool operator !=(Quantity q1, int q2)
            => q1.Value != q2;
    }
}
