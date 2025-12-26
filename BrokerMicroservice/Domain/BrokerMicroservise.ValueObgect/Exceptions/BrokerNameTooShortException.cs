using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerMicroservise.ValueObgect.Exceptions
{
    ///<summary>
    /// Проверка длины имени брокера (слишком короткое значение).
    ///</summary>
    internal class BrokerNameTooShortException(string name, int minLength)
        : ArgumentException($"Название брокера слишком короткое (минимум {minLength} символа): \"{name}\".", nameof(name))
    {
        public string Name => name;
        public int MinLength => minLength;
    }
}
