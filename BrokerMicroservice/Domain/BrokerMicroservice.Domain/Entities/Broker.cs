using BrokerMicroservice.Domain.Entities.Base;
using BrokerMicroservice.Domain.Enums;
using BrokerMicroservice.Domain.Exceptions;
using BrokerMicroservise.ValueObgect;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using static System.Collections.Specialized.BitVector32;

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

        public Asset? EditAsset(Asset asset, MinimalUnit unit, Money price)
        {
            if (asset == null) return null;
            var isEdit = asset.ChangePurchasePrice(price) || asset.ChangeMinimalUnit(unit);

            return isEdit ? asset : null;
        }

        public bool DeleteAsset(Asset asset)
        {
            if (asset == null) return false;
            if (!_asset.Contains(asset)) return false;
            _asset.Remove(asset);
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
            if (client == null) return false;
            if (_client.Contains(client)) //Проверка на то что клиент не повторяется 
                throw new AddingAnExistingClientException(this.Name, client.Id, client.FirstName, client.LastName, client.MiddleName);
            
            _client.Add(client);
            return true;
        }

        /// <summary>
        /// Удаление клиента из списка брокера (информацию о клиенте далее удаляется в сервисе)
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool DeleteClient(Client client)
        {
            if (client == null) return false;
            if (!_client.Contains(client)) return false;
            client.ClearTransactions();
            _client.Remove(client);
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
            if (asset == null) return false; 
            // Запрещаем добавлять актив с уже существующим типом
            if (_asset.Any(a => a.AssetType == asset.AssetType))
            throw new AddingAnExistingAssetException(this.Name, asset.AssetType);
        
            _asset.Add(asset);
            return true;
        }

        /// <summary>
        /// Создаём объект актива (оболочку)
        /// </summary>
        /// <param name="assetType"></param>
        /// <param name="minimalUnit"></param>
        /// <param name="purchasePrice"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullValueException"></exception>
        public Asset StartAsset(AssetType assetType, MinimalUnit minimalUnit, Money purchasePrice)
        {
            if (minimalUnit == null) 
                throw new ArgumentNullValueException(nameof(minimalUnit));
            if (purchasePrice == null)
                throw new ArgumentNullValueException(nameof(purchasePrice));
            Asset asset = new Asset(assetType, minimalUnit, purchasePrice, this);
            AddAsset(asset);
            return asset;
        }


        /// <summary>
        /// Редактирование Имени Брокера
        /// </summary>
        /// <param name="newName"></param>
        /// <returns></returns>
        public bool ChangeBrokerName(BrokerName newName)
        {
            if (Name == newName) return false;
            Name = newName;
            return true;
        }

        /// <summary>
        /// Генерация случайной цифровой строки нужной длины.
        /// </summary>
        private static string GenerateDigits(int length)
        {
            var bytes = new byte[length];
            RandomNumberGenerator.Fill(bytes);

            var chars = new char[length];
            for (int i = 0; i < length; i++)
                chars[i] = (char)('0' + (bytes[i] % 10));

            return new string(chars);
        }

        /// <summary>
        /// Создание карты
        /// </summary>
        /// <returns></returns>
        private Card CreateCard()
        {
            // Генерируем номер карты, пока не найдём уникальный среди клиентов брокера
            while (true)
            {
                var numberString = GenerateDigits(16);  //16 цифр
                var cardNumber = new CardNumber(numberString);

                var exists = _client.Any(c => c.Card.CardNumber.Equals(cardNumber));
                if (!exists)
                    return new Card(cardNumber);
            }
        }

        /// <summary>
        /// Создание портфеля
        /// </summary>
        /// <returns></returns>
        private Portfolio CreatePortfolio()
        {
            //Генерируем номер портфеля, пока не найдём уникальный среди клиентов брокера
            while (true)
            {
                var numberString = GenerateDigits(12);   //12 цифр (пример)
                var portfolioNumber = new PortfolioNumber(numberString);

                var exists = _client.Any(c => c.Portfolio.PortfolioNumber.Equals(portfolioNumber));
                if (!exists)
                    return new Portfolio(portfolioNumber);
            }
        }

        /// <summary>
        /// Создаёт аккаунт клиента с сгенерированной уникальной картой и уникальным номером портфеля
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="middleName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Client CreateClient(LastName lastName, FirstName firstName, MiddleName? middleName, PhoneNumber phoneNumber, Email email)
        {
            var card = CreateCard();
            var portfolio = CreatePortfolio();

            var client = new Client(firstName, lastName, middleName, email, phoneNumber, card, portfolio, this);
            AddClient(client);
            return client;
        }
        #endregion
    }
}
