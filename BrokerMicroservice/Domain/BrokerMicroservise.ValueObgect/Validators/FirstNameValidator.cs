using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Exceptions;

namespace BrokerMicroservise.ValueObgect.Validators
{
    public class FirstNameValidator : IValidator<string>
    {
        /// <summary>
        /// Максимальная длина имени
        /// </summary>
        public static int MAX_LENGTH => 55;
        /// <summary>
        /// Минимальная длина имени
        /// </summary>
        public static int MIN_LENGTH => 2;

        /// <summary>
        /// Проверяет строку, чтобы убедиться, что она не является нулевой, пустой и не состоит только из пробелов.
        /// </summary>
        /// <param name="value">Строка, в которой находятся данные.</param>
        /// <exception cref="ArgumentNullOrWhiteSpaceException">Исключение, которое создаётся если, строка нулевая или состоит из пробелов.</exception>
        /// <exception cref="FirstNameLongValueException">Исключение, которое создаётся, если длина имени больше допустимой длины.</exception>
        /// <exception cref="FirstNameShortValueException">Исключение, которое создаётся, если длина имени меньше допустимой длины.</exception>
        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(ExceptionMessages.FIRSTNAME_NOT_NULL_OR_WHITE_SPACE, nameof(value)); // С помощью ключевого слова typeof мы получаем тип класса
            if (value.Length > MAX_LENGTH)
                throw new FirstNameLongValueException(value, MAX_LENGTH);
            if (value.Length < MIN_LENGTH)
                throw new FirstNameShortValueException(value, MIN_LENGTH);
        }
    }
}
