namespace BrokerMicroservise.ValueObgect.Exceptions
{
    /// <summary>
    /// Обеспечивает строковые константы для сообщений об ошибках для исключений
    /// </summary>
    internal static class ExceptionMessages
    {

        public const string VALIDATOR_MUST_BE_SPECIFIED = "Валидатор для типа должен быть указан";
        public const string LASTNAME_NOT_NULL_OR_WHITE_SPACE = "Фамилия не должна быть нулевой, пустой или состоять только из символов пробела";
        public const string FIRSTNAME_NOT_NULL_OR_WHITE_SPACE = "Имя не должно быть нулевым, пустым или состоять только из символов пробела";
        public const string PHONENUMBER_NOT_NULL_OR_WHITE_SPACE = "Номер телефона не должен быть нулевым, пустым или состоять только из символов пробела";
        public const string CARD_NUMBER_NOT_NULL_OR_WHITE_SPACE = "Номер карты не должен быть нулевым, пустым или состоять только из символов пробела";
        public const string MONEY_AMOUNT_NON_POSITIVE = "Сумма не должна быть отрицательной";
        public const string EMAIL_NOT_NULL_OR_WHITE_SPACE = "EMAIL не должен быть нулевым, пустым или состоять только из символов пробела";
        public const string BROKER_NAME_NOT_NULL_OR_WHITE_SPACE = "Имя брокера не должно быть нулевым, пустым или состоять только из символов пробела";
        public const string MONEY_AMOUNT_HAS_NOT_MORE_THEN_TWO_DECIMAL_PLACES = "Сумма денег не должна быть с более, чем двумя знаками после запятой";
        public const string PORTFOLIO_NUMBER_NOT_NULL_OR_WHITE_SPACE = "Номер портфеля не должен быть нулевым, пустым или состоять только из символов пробела";

    }
}
