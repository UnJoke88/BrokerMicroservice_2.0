namespace BrokerMicroservice.Domain.Enums
{
    /// <summary>
    /// Статус транзакции
    /// </summary>
    public enum TransactionStatus
    {

        /// <summary>
        /// Транзакция успешно завершена
        /// </summary>
        Completed = 1,

        /// <summary>
        /// Транзакция не удалась (ошибка, отмена и т.д.)
        /// </summary>
        Failed = 2
    }
}
