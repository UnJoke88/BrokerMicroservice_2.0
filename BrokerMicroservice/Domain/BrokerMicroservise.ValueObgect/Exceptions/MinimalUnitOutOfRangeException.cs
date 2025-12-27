using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerMicroservise.ValueObgect.Exceptions
{
    internal class MinimalUnitOutOfRangeException(int value)
        : ArgumentOutOfRangeException(nameof(value), value, "Минимальная единица актива не может быть меньше 1.")
    {
        public int Value => value;
    }
}
