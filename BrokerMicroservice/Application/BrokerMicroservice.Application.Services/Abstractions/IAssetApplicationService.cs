using BrokerMicroservice.Application.Models.Asset;


namespace BrokerMicroservice.Application.Services.Abstractions
{
    public interface IAssetApplicationService
    {
        Task<IEnumerable<AssetModel>> GetAssetsAsync(CancellationToken cancellationToken = default);

        Task<AssetModel?> GetAssetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<AssetModel?> CreateAssetAsync(CreateAssetModel assetInformation, CancellationToken cancellationToken = default);
        Task<bool> UpdateAssetAsync(AssetModel assetInformation, CancellationToken cancellationToken = default);
        Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default);

    }
}
