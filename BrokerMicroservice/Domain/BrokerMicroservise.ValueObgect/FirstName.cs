using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Validators;

namespace BrokerMicroservise.ValueObgect
{
    /// <summary>
    /// Представляет тип имени сущности (покупателя).
    /// </summary>
    /// <param name="name">Имя сущности.</param>
    public class FirstName(string name)
        : ValueObject<string>(new FirstNameValidator(), name); // Наследование от базовой сущности. При создании объекта класса, проверяем(валидируем) его 

}
