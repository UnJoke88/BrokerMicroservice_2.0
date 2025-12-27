namespace BrokerMicroservice.Domain.Enums
{
    /// <summary>
    /// Тип транзакции
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Снятие
        /// </summary>
        Removing,

        /// <summary>
        /// Пополнение
        /// </summary>
        Replenishment,

        /// <summary>
        /// Продажа
        /// </summary>
        Sale,

        /// <summary>
        /// Покупка
        /// </summary>
        Purchase
    }
}
