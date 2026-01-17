using BrokerMicroservice.WebHost.Requests.Asset;
using BrokerMicroservice.WebHost.Validators.Base;
using FluentValidation;

namespace BrokerMicroservice.WebHost.Validators.Asset
{

    public class UpdateAssetValidator : AbstractValidator<UpdateAssetRequest>
    {
        public UpdateAssetValidator()
        {
            RuleFor(asset => asset.Id)
                .SetValidator(new GuidPresentationValidator());

            RuleFor(asset => asset.MinimalUnit)
                .SetValidator(new MinimalUnitPresentationValidator());

            RuleFor(asset => asset.PurchasePrice)
                .SetValidator(new MoneyAmountPresentationValidator());
        }
    }
}
