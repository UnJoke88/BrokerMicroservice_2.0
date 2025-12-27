namespace BrokerMicroservise.ValueObgect.Exceptions
{
    internal class FirstNameShortValueException(string firstName, int minLength)
                   : FormatException($"First name length {firstName} less than minimum allowed(допустимая длина) length {minLength}") // FormatException. Исключение, которое возникает в случае, если формат аргумента недопустим или строка составного формата построена неправильно.  Наследование Object ==> Exception ==> SystemException ==> FormatException
    {
        public string FirstName => firstName;
        public int MinLength => minLength;
    }
}
