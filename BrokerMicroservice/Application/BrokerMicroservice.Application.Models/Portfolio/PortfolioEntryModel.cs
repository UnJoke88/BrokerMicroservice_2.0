using BrokerMicroservice.Application.Models.Base;

namespace BrokerMicroservice.Application.Models.Portfolio
{
    /// <summary>
    /// DTO позиции портфеля (строка в портфеле клиента).
    /// Нужна для отображения на сайте: актив, количество, цена и итоговая стоимость позиции.
    /// </summary>
    public sealed record class PortfolioEntryModel : IModel<Guid>
    {
        /// <summary>
        /// Id записи портфеля (PortfolioEntry.Id).
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Id актива (PortfolioEntry.AssetId).
        /// </summary>
        public Guid AssetId { get; init; }

        /// <summary>
        /// Тип актива (берём из Asset.AssetType).
        /// </summary>
        public AssetType AssetType { get; init; }

        /// <summary>
        /// Количество актива в портфеле (PortfolioEntry.Quantity).
        /// В DTO храним как число.
        /// </summary>
        public decimal Quantity { get; init; }

        /// <summary>
        /// Цена за единицу (Asset.PurchasePrice).
        /// </summary>
        public decimal UnitPrice { get; init; }

        /// <summary>
        /// Итог по позиции: Quantity * UnitPrice.
        /// </summary>
        public decimal TotalValue { get; init; }

    }
    public enum AssetType
    {
        USD = 1,
        EUR = 2,
        RUB = 3,
        CNY = 4,
        GBP = 5,
        GOLD = 6,
        SILVER = 7,
        ALUMINUM = 8,
        OIL = 9,
        GAS = 10
    }
}

