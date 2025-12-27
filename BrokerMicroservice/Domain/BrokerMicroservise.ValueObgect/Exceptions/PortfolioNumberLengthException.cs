namespace BrokerMicroservise.ValueObgect.Exceptions
{
    internal class PortfolioNumberLengthException(string portfolioNumber, int length)
            : FormatException($"Длина имени портфеля под номером {portfolioNumber} не равна допустимой длине {length}")
    {
        public string PortfolioNumber => portfolioNumber;
        public int Length => length;
    }
}
