namespace BrokerMicroservise.ValueObgect.Exceptions
{
    internal class FirstNameLongValueException(string firstName, int maxLength)
      : FormatException($"First name length {firstName} greated than maximum allowed(допустимая длина) length {maxLength}") // FormatException. Исключение, которое возникает в случае, если формат аргумента недопустим или строка составного формата построена неправильно.  Наследование Object ==> Exception ==> SystemException ==> FormatException
    {
        public string FirstName => firstName;
        public int MaxLength => maxLength;
    }
}
