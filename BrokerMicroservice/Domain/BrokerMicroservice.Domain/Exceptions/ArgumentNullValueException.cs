namespace BrokerMicroservice.Domain.Exceptions
{
    public class ArgumentNullValueException(string paramName)
          : ArgumentNullException(paramName, $"Argument {paramName} value is null");
    //Это исключение выходит, если один из передаваемых методу аргументов является недопустимым.
}
