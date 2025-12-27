using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Exceptions;

namespace BrokerMicroservise.ValueObgect.Validators
{
    public class MinimalUnitValidator : IValidator<int>
    {
        public void Validate(int value)
        {
            if (value < 1)
                throw new MinimalUnitOutOfRangeException(value);
        }
    }
}
