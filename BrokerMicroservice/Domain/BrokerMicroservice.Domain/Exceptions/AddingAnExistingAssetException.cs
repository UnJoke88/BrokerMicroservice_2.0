using BrokerMicroservice.Domain.Enums;
using BrokerMicroservise.ValueObgect;

namespace BrokerMicroservice.Domain.Exceptions
{
    public class AddingAnExistingAssetException(BrokerName name, AssetType assetType)
        : InvalidOperationException($"Тип актива {assetType} Уже существует в списке брокера {name} ")
    //InvalidOperationException - Исключение, которое выдается при вызове метода, недопустимого для текущего состояния объекта.
    {
        public BrokerName Name => name;
        public AssetType AssetType => assetType;
    }
}
