using BrokerMicroservice.Domain.Enums;
using BrokerMicroservise.ValueObgect;

namespace BrokerMicroservice.Domain.Exceptions
{
    public class SellingMoreAssetsThanInPortfolioException(Guid transactionId, AssetType AssetType, Quantity Quantity)
           : InvalidOperationException($"Транзакция с номером {transactionId} не осуществилась: Вы хоите продать большее количество " +
               $"({Quantity}) актива {AssetType}, чем у вас есть в портфеле")
    //InvalidOperationException - Исключение, которое выдается при вызове метода, недопустимого для текущего состояния объекта.
    {
        //Прописываем поля класса для просмотра ошибки в них (просмотр обычно осуществляется в program при вызове)
        public Guid transactionId => transactionId;
        public Quantity Quantity => Quantity;
        public AssetType AssetType => AssetType;

    }
}
