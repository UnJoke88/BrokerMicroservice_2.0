using AutoMapper;
using BrokerMicroservice.Application.Models.Asset;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.Domain.Entities;
using BrokerMicroservice.Repositories.Abstractions;

namespace BrokerMicroservice.Application.Services
{
    public class AssetApplicationService(IRepository<Asset, Guid> assetRepository, IRepository<Broker, Guid> brokerRepository, IMapper mapper)
         : IAssetApplicationService
    {
        public async Task<IEnumerable<AssetModel>> GetAssetsAsync(CancellationToken cancellationToken = default)
                => (await assetRepository.GetAllAsync(cancellationToken = default, true))
                .Select(mapper.Map<AssetModel>);

        public async Task<AssetModel?> GetAssetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var asset = await assetRepository.GetByIdAsync(id, cancellationToken);
            return asset is null ? null : mapper.Map<AssetModel>(asset);
        }

        public async Task<AssetModel?> CreateAssetAsync(CreateAssetModel assetInformation, CancellationToken cancellationToken = default)
        {
            //брокер, создаёт модель
            var broker = await brokerRepository.GetByIdAsync(assetInformation.BrokerId, cancellationToken);
            if (broker is null)
                return null;

            //Добавляем актив в список брокера  
            var asset = broker.StartAsset((Domain.Enums.AssetType)assetInformation.AssetType, 
                new(assetInformation.MinimalUnit),
                new(assetInformation.PurchasePrice));

            if (asset is null)
                return null;

            //Обновление брокера
            var updatedBroker = await brokerRepository.UpdateAsync(broker, cancellationToken); 

            // Добавление списка активов в бд
            var createdAsset = await assetRepository.AddAsync(asset, cancellationToken);
            return createdAsset is null ? null : mapper.Map<AssetModel>(createdAsset);
        }

        public async Task<bool> UpdateAssetAsync(AssetModel assetInformation, CancellationToken cancellationToken = default)
        {
            var brokerTask = brokerRepository.GetByIdAsync(assetInformation.BrokerId, cancellationToken);
            if (brokerTask is null)
                return false;
            var broker = brokerTask.Result;

            var assetTask = assetRepository.GetByIdAsync(assetInformation.Id, cancellationToken);
            if (assetTask is null)
                return false;
            var asset = assetTask.Result;

            var editAsset = broker.EditAsset(asset!, new(assetInformation.MinimalUnit),new(assetInformation.PurchasePrice));
            if (editAsset is null)
                return false;

            return await assetRepository.UpdateAsync(editAsset, cancellationToken);
        }

        public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var asset = await assetRepository.GetByIdAsync(id, cancellationToken);
            if (asset is null)
                return false;
            var broker = await brokerRepository.GetByIdAsync(asset.Broker.Id, cancellationToken);
            if (broker is null)
                return false;
            var isAssetClear = broker.DeleteAsset(asset);

            var updatedBroker = await brokerRepository.UpdateAsync(broker, cancellationToken); // Обновление администратора

            return isAssetClear ? await assetRepository.DeleteAsync(asset, cancellationToken) : false;
        }

    }
}
