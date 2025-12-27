using BrokerMicroservice.Domain.Entities.Base;
using BrokerMicroservice.Domain.Exceptions;
using BrokerMicroservice.Domain.Enums;
using BrokerMicroservise.ValueObgect;

namespace BrokerMicroservice.Domain.Entities
{
    public class Broker : Entity<Guid>
    {
        #region Свойства

        public BrokerName Name { get; private set; }

        private readonly ICollection<Client> _client = [];

        private readonly ICollection<Asset> _asset = [];

        #endregion

        #region Конструктор

        protected Broker()
        {

        }

        protected Broker(Guid id, BrokerName name)
            : base(id)
        {
            Name = name ?? throw new ArgumentNullValueException(nameof(name));
        }

        public Broker(BrokerName name)
           : this(Guid.NewGuid(), name)
        {

        }
        #endregion

        #region Методы

        /// <summary>
        /// Изменить Брокеру минимальное кол-во актива за одну покупку
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool SetUnit(Asset asset, MinimalUnit unit)
        {
            if (asset == null) return false;
            if (!asset.ChangeMinimalUnit(unit)) return false;
            return true;
        }

        /// <summary>
        /// Брокер меняет цену на актив
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public bool SetPrice(Asset asset, Money price)
        {
            if (asset == null) return false;
            if (!asset.ChangePurchasePrice(price)) return false;
            return true;
        }

        /// <summary>
        /// Получение Список всех клиентов
        /// </summary>
        public IReadOnlyCollection<Client> ShowClients => //ShowClients - название коллекции клиентов
            _client.ToList().AsReadOnly();

        /// <summary>
        /// Добавление клиента в список брокера (в управление брокером)
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool AddClient(Client client)
        {
            if (_client.Contains(client)) //Проверка на то что клиент не повторяется 
                throw new AddingAnExistingClientException(this.Name, client.Id, client.FirstName, client.LastName, client.MiddleName);
            if (client == null) return false;
            _client.Add(client);
            return true;
        }


        /// <summary>
        /// Получение Список всех активов
        /// </summary>
        public IReadOnlyCollection<Asset> ShowAsset => //ShowClients - название коллекции клиентов
            _asset.ToList().AsReadOnly();

        /// <summary>
        /// Добавление актива как настраиваемый объект в список брокера (в управление брокером)
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public bool AddAsset(Asset asset)
        {
            if (_asset.Contains(asset) && _asset.Any(a => a.AssetType == asset.AssetType))  // Объект не найден, но тип уже есть
                throw new AddingAnExistingAssetException(this.Name, asset.AssetType);
            if (asset == null) return false;
            _asset.Add(asset);
            return true;
        }


        /// <summary>
        /// Редактирование Имени Брокера
        /// </summary>
        /// <param name="newName"></param>
        /// <returns></returns>
        internal bool ChangeBrokerName(BrokerName newName)
        {
            if (Name == newName) return false;
            Name = newName;
            return true;
        }

        #endregion
    }
}
