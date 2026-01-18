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
                => (await assetRepository.GetAllAsync(cancellationToken, true))
                .Select(a => mapper.Map<AssetModel>(a));

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

            // Добавление списка активов в бд
            var createdAsset = await assetRepository.AddAsync(asset, cancellationToken);
            return createdAsset is null ? null : mapper.Map<AssetModel>(createdAsset);
        }

        public async Task<bool> UpdateAssetAsync(AssetModel assetInformation, CancellationToken cancellationToken = default)
        {
            var asset = await assetRepository.GetByIdAsync(assetInformation.Id, cancellationToken);
            if (asset is null) return false;

            // brokerId лучше брать из asset, а не из модели (в модели он может быть пустой Guid)
            var broker = await brokerRepository.GetByIdAsync(asset.BrokerId, cancellationToken);
            if (broker is null) return false;

            var edited = broker.EditAsset(asset,
                new(assetInformation.MinimalUnit),
                new(assetInformation.PurchasePrice));

            if (edited is null) return false;

            // иногда нужно ещё сохранить брокера, если он реально хранит/проверяет список активов
            await brokerRepository.UpdateAsync(broker, cancellationToken);

            return await assetRepository.UpdateAsync(edited, cancellationToken);
        }

        public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var asset = await assetRepository.GetByIdAsync(id, cancellationToken);
            if (asset is null)
                return false;

            return await assetRepository.DeleteAsync(asset, cancellationToken);
        }

    }
}
