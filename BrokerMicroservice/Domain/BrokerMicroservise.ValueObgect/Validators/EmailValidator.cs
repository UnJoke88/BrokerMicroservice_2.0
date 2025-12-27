using BrokerMicroservise.ValueObgect.Base;
using BrokerMicroservise.ValueObgect.Exceptions;
using System.Net.Mail;

namespace BrokerMicroservise.ValueObgect.Validators
{
    public class EmailValidator : IValidator<string>
    {
        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(ExceptionMessages.EMAIL_NOT_NULL_OR_WHITE_SPACE, nameof(value));

            var address = new MailAddress(value);
            if (address.Address != value)
                throw new EmailFormatException(nameof(value), value);
        }
    }
}
