using BrokerMicroservice.Domain.Entities.Base;
using BrokerMicroservice.Domain.Enums;
using BrokerMicroservice.Domain.Exceptions;
using BrokerMicroservise.ValueObgect;

namespace BrokerMicroservice.Domain.Entities
{
    public class Asset : Entity<Guid>
    {

        #region Свойства

        /// <summary>
        /// Тип актива
        /// </summary>
        public AssetType AssetType { get; }

        /// <summary>
        /// Минимальная единица покупки (например от 1).
        /// </summary>
        public MinimalUnit MinimalUnit { get; private set; }

        /// <summary>
        /// Минимальная цена покупки за единицу
        /// </summary>
        public Money PurchasePrice { get; private set; }

        public Broker Broker { get; private set; }

        #endregion

        #region Конструктор

        protected Asset()
        {

        }

        protected Asset(Guid id, AssetType assetType, MinimalUnit minimalUnit, Money purchasePrice)
            : base(id)
        {
            AssetType = assetType;
            MinimalUnit = minimalUnit ?? throw new ArgumentNullValueException(nameof(minimalUnit));
            PurchasePrice = purchasePrice ?? throw new ArgumentNullValueException(nameof(purchasePrice));
        }

        public Asset(AssetType assetType, MinimalUnit minimalUnit, Money purchasePrice)
            : this(Guid.NewGuid(), assetType, minimalUnit, purchasePrice)
        {

        }

        #endregion

        #region Методы

        /// <summary>
        /// Изменяет минимальную единицу покупки.
        /// </summary>
        /// <param name="newMinimalUnit">Новое значение минимальной единицы.</param>
        /// <returns>True, если значение изменено; иначе — false.</returns>
        internal bool ChangeMinimalUnit(MinimalUnit newMinimalUnit)
        {
            if (newMinimalUnit is null)
                throw new ArgumentNullValueException(nameof(newMinimalUnit));

            if (MinimalUnit == newMinimalUnit)
                return false;

            MinimalUnit = newMinimalUnit;
            return true;
        }

        /// <summary>
        /// Изменяет цену покупки.
        /// </summary>
        /// <param name="newPurchasePrice">Новое значение цены покупки.</param>
        /// <returns>True, если значение изменено; иначе — false.</returns>
        internal bool ChangePurchasePrice(Money newPurchasePrice)
        {
            if (newPurchasePrice is null)
                throw new ArgumentNullValueException(nameof(newPurchasePrice));

            if (PurchasePrice == newPurchasePrice)
                return false;

            PurchasePrice = newPurchasePrice;
            return true;
        }

        #endregion
    }
}
