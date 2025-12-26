namespace BrokerMicroservise.ValueObgect.Base
{
    /// <summary>
    /// Интерфейс валидатора объекта-значения.
    /// </summary>
    /// <typeparam name="T">Тип проверяемого значения.</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Метод валидации значения.
        /// </summary>
        /// <param name="value">Проверяемое значение.</param>
        void Validate(T value);
    }
}
