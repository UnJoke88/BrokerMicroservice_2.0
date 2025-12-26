using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerMicroservise.ValueObgect.Exceptions
{
    ///<summary>
    /// Проверка длины имени брокера (слишком длинное значение).
    ///</summary>
    internal class BrokerNameTooLongException(string name, int maxLength)
        : ArgumentException($"Название брокера превышает максимально допустимую длину в {maxLength} символов: \"{name}\".", nameof(name))
    {
        public string Name => name;
        public int MaxLength => maxLength;
    }
}
