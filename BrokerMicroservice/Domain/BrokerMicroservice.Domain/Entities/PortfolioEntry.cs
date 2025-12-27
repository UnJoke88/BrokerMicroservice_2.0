using BrokerMicroservice.Domain.Entities.Base;
using BrokerMicroservice.Domain.Enums;
using BrokerMicroservice.Domain.Exceptions;
using BrokerMicroservise.ValueObgect;

namespace BrokerMicroservice.Domain.Entities
{
    /// <summary>
    /// Вспомогательная сущность для вывода статистики актива в БД
    /// </summary>
    public class PortfolioEntry : Entity<Guid>
    {
        public Guid AssetId { get; set; }             // Foreign Key
        public Asset Asset { get; set; }              // Навигация

        public Quantity Quantity { get; set; }        // Значение

        public Guid PortfolioId { get; set; }         // Foreign Key
        public Portfolio Portfolio { get; set; }      // Навигация
    }
}
