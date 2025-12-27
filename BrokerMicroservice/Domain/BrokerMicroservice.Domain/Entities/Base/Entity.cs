namespace BrokerMicroservice.Domain.Entities.Base
{
    /// <summary>
    /// Базовая сущность для всех доменных объектов с уникальным идентификатором.
    /// </summary>
    public abstract class Entity<TId>(TId id) where TId : struct, IEquatable<TId>
    {
        // Идентификатор сущности
        public TId Id { get; protected set; } = id;

        protected Entity() : this(default!)
        {

        }

    }
}
