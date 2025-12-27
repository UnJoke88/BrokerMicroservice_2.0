using BrokerMicroservice.Domain.Entities.Base;
using BrokerMicroservice.Domain.Enums;
using BrokerMicroservice.Domain.Exceptions;
using BrokerMicroservise.ValueObgect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerMicroservice.Domain.Entities
{
    public class Portfolio : Entity<Guid>
    {
        #region Поля

        private readonly ICollection<PortfolioEntry> _entries = new HashSet<PortfolioEntry>();

        public IReadOnlyCollection<PortfolioEntry> AssetEntries => (IReadOnlyCollection<PortfolioEntry>)_entries;
        #endregion

        #region Свойства

        ///<summary> Получить общую стоимость портфеля. </summary>
        public Money TotalValue => GetTotalPortfolioValue();

        ///<summary> Уникальное имя или код портфеля. </summary>
        public PortfolioNumber PortfolioNumber { get; private set; }

        #endregion

        #region Конструкторы

        protected Portfolio() { }

        public Portfolio(PortfolioNumber code) : base(Guid.NewGuid())
        {
            PortfolioNumber = code ?? throw new ArgumentNullValueException(nameof(code));
        }

        #endregion

        #region Методы

        /// <summary>
        /// Применить транзакцию к портфелю (учитываются только покупка/продажа активов)
        /// </summary>
        /// <param name="transaction"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void ApplyTransaction(Transaction transaction)
        {
            if (transaction.Asset == null) return;

            var entry = _entries.FirstOrDefault(e => e.Asset.Id == transaction.Asset.Id); //при покупке — нужно ли увеличить количество уже существующего актива,
                                                                                          //при продаже — достаточно ли актива, чтобы уменьшить количество.

            if (transaction.Type == TransactionType.Purchase)
            {
                if (entry != null)
                    entry.Quantity += transaction.Quantity;
                else
                {
                    _entries.Add(new PortfolioEntry
                    {
                        Asset = transaction.Asset,
                        AssetId = transaction.Asset.Id,
                        Quantity = transaction.Quantity,
                        Portfolio = this,
                        PortfolioId = this.Id
                    });
                }
            }
            else if (transaction.Type == TransactionType.Sale)
            {
                if (entry == null || entry.Quantity < transaction.Quantity)
                    throw new SellingMoreAssetsThanInPortfolioException(transaction.Id, transaction.Asset.AssetType, transaction.Quantity);

                entry.Quantity -= transaction.Quantity;

                if (entry.Quantity.Value == 0)
                    _entries.Remove(entry);
            }
        }

        /// <summary>
        /// Получить статистику по активам в портфеле: тип, количество, общая стоимость.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<(AssetType AssetType, Quantity Quantity, Money TotalValue)> GetAssetStatistics()
        {
            foreach (var entry in _entries)
            {
                var assetType = entry.Asset.AssetType;
                var quantity = entry.Quantity;
                var value = entry.Asset.PurchasePrice * quantity;
                yield return (assetType, quantity, value);
            }
        }


        /// <summary>
        /// Получить общую стоимость портфеля
        /// </summary>
        /// <returns></returns>
        public Money GetTotalPortfolioValue()
        {
            Money total = new(0);
            foreach (var stat in GetAssetStatistics())
            {
                total += stat.TotalValue;
            }
            return total;
        }
        #endregion
    }
}
