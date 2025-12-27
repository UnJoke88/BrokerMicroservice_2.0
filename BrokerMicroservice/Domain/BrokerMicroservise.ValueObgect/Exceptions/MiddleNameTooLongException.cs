namespace BrokerMicroservise.ValueObgect.Exceptions
{
    ///<summary>
    /// Проверка длины отчества.
    ///</summary>
    internal class MiddleNameTooLongException(string name, int maxLength)
        : ArgumentException($"Отчество превышает допустимую длину в {maxLength} символов: \"{name}\".", nameof(name))
    {
        public string Name => name;
        public int MaxLength => maxLength;
    }
}
