using BrokerMicroservice.Domain.Entities.Base;
using BrokerMicroservice.Domain.Enums;
using BrokerMicroservice.Domain.Exceptions;
using BrokerMicroservise.ValueObgect;

namespace BrokerMicroservice.Domain.Entities
{
    /// <summary>
    /// Представляет карту клиента для операций с балансом.
    /// </summary>
    public class Card : Entity<Guid>
    {
        #region Свойства

        /// <summary>
        /// Уникальный номер или название карты.
        /// </summary>
        public CardNumber CardNumber { get; private set; }

        /// <summary>
        /// Получить денежный баланс клиента.
        /// </summary>
        public Money CashBalance { get; private set; }

        #endregion


        #region Конструктор
        protected Card()
        {

        }

        protected Card(Guid id, CardNumber cardNumber)
            : base(id)
        {
            CardNumber = cardNumber ?? throw new ArgumentNullValueException(nameof(cardNumber));
            CashBalance = new Money(0);
        }

        public Card(CardNumber cardNumber)
            : this(Guid.NewGuid(), cardNumber)
        {

        }

        #endregion

        #region Методы

        /// <summary>
        /// Пополняет баланс карты, если тип транзакции — Replenishment .
        /// </summary>
        /// <param name="amount">Сумма пополнения.</param>
        /// <param name="type">Тип транзакции.</param>
        /// <returns>True, если операция выполнена; иначе — false.</returns>
        public bool MakeDeposit(Money amount, TransactionType type)
        {
            if (amount is null || type != TransactionType.Replenishment)
                return false;

            CashBalance = CashBalance + amount;
            return true;
        }

        /// <summary>
        /// Пополняет баланс карты при ПРОДАЖЕ АКТИВА, если тип транзакции — Sale .
        /// </summary>
        /// <param name="amount">Сумма пополнения.</param>
        /// <param name="type">Тип транзакции.</param>
        /// <returns>True, если операция выполнена; иначе — false.</returns>
        public bool MakeSale(Money amount, TransactionType type)
        {
            if (amount is null || type != TransactionType.Sale)
                return false;

            CashBalance = CashBalance + amount;
            return true;
        }

        /// <summary>
        /// Снимает средства с карты, если тип транзакции — Removing и достаточно средств.
        /// </summary>
        /// <param name="amount">Сумма для снятия.</param>
        /// <param name="type">Тип транзакции.</param>
        /// <returns>True, если операция выполнена.</returns>
        public bool MakeWithdraw(Money amount, TransactionType type)
        {
            if (amount is null || type != TransactionType.Removing || CashBalance < amount)
                return false;

            CashBalance = CashBalance - amount;
            return true;
        }

        /// <summary>
        /// Снимает средства с карты на ПОКУПКУ АКТИВА , если тип транзакции — Purchase и достаточно средств.
        /// </summary>
        /// <param name="amount">Сумма для снятия.</param>
        /// <param name="type">Тип транзакции.</param>
        /// <returns>True, если операция выполнена.</returns>
        public bool MakePurchase(Money amount, TransactionType type)
        {
            if (amount is null || type != TransactionType.Purchase || CashBalance < amount)
                return false;

            CashBalance = CashBalance - amount;
            return true;
        }



        #endregion
    }
}
