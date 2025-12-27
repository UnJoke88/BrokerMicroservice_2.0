using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Exceptions;

namespace BrokerMicroservise.ValueObgect.Validators
{
    public class QuantityValidator : IValidator<int>
    {
        public void Validate(int value)
        {
            if (value < 0)
                throw new QuantityNotPositiveException(value);
        }
    }
}
