namespace BrokerMicroservise.ValueObgect.Exceptions
{
    ///<summary>
    /// Проверка формата номера карты.
    ///</summary>
    internal class PortfolioNumberFormatException(string paramName, string portfolioNumber)
        : ArgumentException($"Номер портфеля имеет недопустимый формат: \"{portfolioNumber}\".", paramName)
    {
        public string PortfolioNumber => portfolioNumber;
    }
}
