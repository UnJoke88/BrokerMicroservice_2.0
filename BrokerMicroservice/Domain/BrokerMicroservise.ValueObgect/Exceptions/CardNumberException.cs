namespace BrokerMicroservise.ValueObgect.Exceptions
{   
    ///<summary>
    /// Проверка формата номера карты.
    ///</summary>
    internal class CardNumberFormatException(string paramName, string cardNumber)
        : ArgumentException($"Номер карты имеет недопустимый формат: \"{cardNumber}\".", paramName)
    {
        public string CardNumber => cardNumber;
    }
}
