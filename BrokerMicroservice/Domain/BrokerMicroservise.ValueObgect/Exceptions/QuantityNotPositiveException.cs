namespace BrokerMicroservise.ValueObgect.Exceptions
{
    ///<summary>
    /// Проверка диапазона значения минимальной единицы.
    ///</summary>
    internal class QuantityNotPositiveException(int value)
        : ArgumentOutOfRangeException(nameof(value), value, "Количество не может быть меньше 0.")
    {
        public int Value => value;
    }
}
