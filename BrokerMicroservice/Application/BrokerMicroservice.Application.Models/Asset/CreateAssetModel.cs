using BrokerMicroservice.Application.Models.Base;


namespace BrokerMicroservice.Application.Models.Asset
{

    /// <summary>
    /// DTO для создания актива (то, что приходит из Presentation и уходит в Application).
    /// AutoMapper может спокойно создать такой объект через init-свойства.
    /// </summary>
    public sealed class CreateAssetModel : ICreateModel
    {
        /// <summary>Тип актива (enum).</summary>
        public AssetType AssetType { get; init; }

        /// <summary>Минимальная единица покупки.</summary>
        public int MinimalUnit { get; init; }

        /// <summary>Цена покупки (в домене это Money, тут decimal).</summary>
        public decimal PurchasePrice { get; init; }

        /// <summary>
        /// Id брокера-владельца (если у тебя Asset привязан к Broker).
        /// Если сейчас в запросе ты это не передаёшь — либо сделай поле nullable,
        /// либо бери BrokerId из маршрута/контекста.
        /// </summary>
        public Guid BrokerId { get; init; } // или Guid? если пока не передаёшь
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
