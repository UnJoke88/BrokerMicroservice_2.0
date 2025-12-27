namespace BrokerMicroservise.ValueObgect.Exceptions
{
    ///<summary>
    /// Проверка формата email-адреса.
    ///</summary>
    internal class EmailFormatException(string paramName, string email)
        : ArgumentException($"Email имеет недопустимый формат: \"{email}\".", paramName)
    {
        public string Email => email;
    }
}
