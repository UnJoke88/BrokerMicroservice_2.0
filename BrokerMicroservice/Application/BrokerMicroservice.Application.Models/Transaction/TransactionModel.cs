using BrokerMicroservice.Application.Models.Base;


namespace BrokerMicroservice.Application.Models.Transaction
{
    public record class TransactionModel(Guid Id, Guid ClientId, DateTime Date, TransactionType Type, Guid? AssetId, int? Quantity, decimal Amount,
        TransactionStatus Status, decimal EndBalance) : IModel<Guid>;



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
