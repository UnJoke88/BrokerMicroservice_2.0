using BrokerMicroservice.Domain.Entities.Base;
using BrokerMicroservice.Domain.Enums;
using BrokerMicroservice.Domain.Exceptions;
using BrokerMicroservise.ValueObgect;

namespace BrokerMicroservice.Domain.Entities
{
    public class Client : Entity<Guid>
    {
        #region Свойства

        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public MiddleName? MiddleName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; }

        public Card Card { get; }

        public Portfolio Portfolio { get; }

        private readonly ICollection<Transaction> _transactions = [];
        public Guid BrokerId { get; private set; }
        public Broker Broker { get; private set; }
        public Guid CardId { get; private set; }
        public Guid PortfolioId { get; private set; }

        #endregion

        #region Конструктор
        protected Client()
        {

        }

        protected Client(Guid id, FirstName firstName, LastName lastName, MiddleName? middleName,
                      Email email, PhoneNumber phoneNumber, Card card, Portfolio portfolio)
            : base(id)
        {
            FirstName = firstName ?? throw new ArgumentNullValueException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullValueException(nameof(lastName));
            MiddleName = middleName;
            Email = email ?? throw new ArgumentNullValueException(nameof(email));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullValueException(nameof(phoneNumber));
            Card = card ?? throw new ArgumentNullValueException(nameof(card));
            Portfolio = portfolio ?? throw new ArgumentNullValueException(nameof(portfolio));
        }

        public Client(FirstName firstName, LastName lastName, MiddleName? middleName,
                      Email email, PhoneNumber phoneNumber, Card card, Portfolio portfolio)
            : this(Guid.NewGuid(), firstName, lastName, middleName, email, phoneNumber, card, portfolio)//Guid.NewGuid() - формирует id и передаёт в Guid id(protected) далее base(id)
        {

        }
        #endregion

        #region Методы

        /// <summary>
        /// Получение всех транзакций
        /// </summary>
        public IReadOnlyCollection<Transaction> ShowTransactions => //ShowTransactions - название коллекции транзакций
            _transactions.ToList().AsReadOnly();


        /// <summary>
        /// Покупка Актива
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Transaction BuyAsset(Asset asset, Quantity quantity)  //Quantity - количество единиц актива
        {
            var amount = asset.PurchasePrice * quantity;
            var status = this.Card.MakePurchase(amount, TransactionType.Purchase) ? TransactionStatus.Completed : TransactionStatus.Failed; //Сохраняем в переменную результат метода списания денег.=>
                                                                                                                                            //Если получилось снять и нет ошибок = запись в переменную Complited, если нет, то запись Failed
            var transaction = new Transaction(this, DateTime.Now, TransactionType.Purchase, asset, quantity);
            transaction.SetTransactionStatus(status);
            _transactions.Add(transaction);
            if (transaction.Status == TransactionStatus.Completed)
            {
                Portfolio.ApplyTransaction(transaction);
            }

            return transaction;
        }

        /// <summary>
        /// Продажа Актива
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Transaction MakeSale(Asset asset, Quantity quantity)
        {
            var amount = asset.PurchasePrice * quantity;
            var status = this.Card.MakeSale(amount, TransactionType.Sale) ? TransactionStatus.Completed : TransactionStatus.Failed; //Сохраняем в переменную результат метода списания денег.=>
                                                                                                                                    //Если получилось снять и нет ошибок = запись в переменную Complited, если нет, то запись Failed
            var transaction = new Transaction(this, DateTime.Now, TransactionType.Sale, asset, quantity);
            transaction.SetTransactionStatus(status);
            _transactions.Add(transaction);
            if (transaction.Status == TransactionStatus.Completed)
            {
                Portfolio.ApplyTransaction(transaction);
            }

            return transaction;
        }


        /// <summary>
        /// Пополнение карты клиентом и создание транзакции
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Transaction MakeDeposit(Money amount)
        {
            var status = this.Card.MakeDeposit(amount, TransactionType.Replenishment) ? TransactionStatus.Completed : TransactionStatus.Failed;
            var transaction = new Transaction(this, DateTime.Now, TransactionType.Replenishment, amount);
            transaction.SetTransactionStatus(status);
            _transactions.Add(transaction);

            return transaction;
        }

        /// <summary>
        /// Снятие с карты клиентом и создание транзакции
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Transaction MakeWithdraw(Money amount)
        {
            var status = this.Card.MakeWithdraw(amount, TransactionType.Removing) ? TransactionStatus.Completed : TransactionStatus.Failed;
            var transaction = new Transaction(this, DateTime.Now, TransactionType.Removing, amount);
            transaction.SetTransactionStatus(status);
            _transactions.Add(transaction);

            return transaction;
        }



        /// <summary>
        /// Редактирование имени
        /// </summary>
        /// <param name="newFirstName"></param>
        /// <returns></returns>
        internal bool ChangeUsername(FirstName newFirstName)
        {
            if (FirstName == newFirstName) return false;
            FirstName = newFirstName;
            return true;
        }

        /// <summary>
        /// Редактирование Фамилии
        /// </summary>
        /// <param name="newLastName"></param>
        /// <returns></returns>
        internal bool ChangeLastName(LastName newLastName)
        {
            if (LastName == newLastName) return false;
            LastName = newLastName;
            return true;
        }

        /// <summary>
        /// Редактирование Отчества
        /// </summary>
        /// <param name="newMiddleName"></param>
        /// <returns></returns>
        internal bool ChangeMiddleName(MiddleName newMiddleName)
        {
            if (MiddleName == newMiddleName) return false;
            MiddleName = newMiddleName;
            return true;
        }

        /// <summary>
        /// Редактирование Почты
        /// </summary>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        internal bool ChangeEmail(Email newEmail)
        {
            if (Email == newEmail) return false;
            Email = newEmail;
            return true;
        }

        #endregion
    }
}
