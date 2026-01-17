using BrokerMicroservice.WebHost.Requests.Asset;
using BrokerMicroservice.WebHost.Validators.Base;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Asset
{
    public class CreateAssetValidator : AbstractValidator<CreateAssetRequest>
    {
        public CreateAssetValidator()
        {
            RuleFor(asset => asset.AssetType).IsInEnum().WithMessage("Не существует перечисления");

            RuleFor(asset => asset.MinimalUnit)
                .SetValidator(new MinimalUnitPresentationValidator());

            RuleFor(asset => asset.PurchasePrice)
                .SetValidator(new MoneyAmountPresentationValidator());
        }
    }
}
