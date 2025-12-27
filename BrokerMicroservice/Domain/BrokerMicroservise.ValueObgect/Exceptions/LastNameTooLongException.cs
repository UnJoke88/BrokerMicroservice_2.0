namespace BrokerMicroservise.ValueObgect.Exceptions
{
    internal class LastNameTooLongException(string lastName, int maxLength)
         : FormatException($"Last name length {lastName} greated than maximum allowed(допустимая длина) length {maxLength}") // FormatException. Исключение, которое возникает в случае, если формат аргумента недопустим или строка составного формата построена неправильно.  Наследование Object ==> Exception ==> SystemException ==> FormatException
    {
        public string LastName => lastName;
        public int MaxLength => maxLength;
    }
}
