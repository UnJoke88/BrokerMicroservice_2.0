namespace BrokerMicroservise.ValueObgect.Exceptions
{
    ///<summary>
    /// Проверка допустимости символов номера телефона.
    ///</summary>
    internal class PhoneNumberFormatException(string phone)
        : ArgumentException($"Номер телефона имеет недопустимый формат: \"{phone}\".", nameof(phone))
    {
        public string Phone => phone;
    }
}
