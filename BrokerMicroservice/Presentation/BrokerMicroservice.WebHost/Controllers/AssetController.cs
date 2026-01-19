using AutoMapper;
using BrokerMicroservice.Application.Models.Asset;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.WebHost.Requests.Asset;
using BrokerMicroservice.WebHost.Responces.Asset;
using Microsoft.AspNetCore.Mvc;

namespace BrokerMicroservice.WebHost.Controllers
{
    [ApiController]
    [Route("api/Asset/[controller]")]
    public class AssetController(IAssetApplicationService assetApplicationService, IMapper mapper) : ControllerBase
    {
        [HttpGet("Вывод Всех Активов")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AssetShortResponce>))]
        public async Task<IActionResult> GetAllAsset(CancellationToken cancellationToken)
        {
            var asset = await assetApplicationService.GetAssetsAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<AssetShortResponce>>(asset));
        }

        // ShortResponce - для получения краткой информации об сущност.(пример - при возврате списка объектов), DetailedResponce - для получения полной информации об объекте (пример - при выводе информации от idНомеру)?
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetDetailedResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAssetById(Guid id, CancellationToken cancellationToken)
        {
            var asset = await assetApplicationService.GetAssetByIdAsync(id, cancellationToken);
            if (asset is null)
                return NotFound($"Актив с Id: {id} не найден");
            return Ok(mapper.Map<AssetDetailedResponce>(asset));
        }

        [HttpPost("Создание Актива")] 
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AssetShortResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CreateAsset(CreateAssetRequest request, CancellationToken cancellationToken)
        {
            var asset = mapper.Map<CreateAssetModel>(request);
            var createdAsset = await assetApplicationService.CreateAssetAsync(asset, cancellationToken);
            if (createdAsset is null)
                return BadRequest($"Ошибка в создании актива");
            var assetResponce = mapper.Map<AssetShortResponce>(createdAsset);
            return CreatedAtAction(nameof(GetAssetById), new { assetResponce.Id }, assetResponce);
        }

        [HttpPatch("Обновление Актива")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetDetailedResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsset(UpdateAssetRequest request, CancellationToken cancellationToken)
        {
            var asset = await assetApplicationService.GetAssetByIdAsync(request.Id, cancellationToken);
            if (asset is null)
                return NotFound($"Актив с Id:{request.Id} не найден");

            var newAsset = mapper.Map<AssetModel>(request);

            var isAssetUpdated = await assetApplicationService.UpdateAssetAsync(newAsset, cancellationToken);
            if (isAssetUpdated == false)
                return BadRequest($"Невозможно изменить актив");

            var updated = await assetApplicationService.GetAssetByIdAsync(request.Id, cancellationToken);
            return Ok(mapper.Map<AssetDetailedResponce>(updated));
        }

        [HttpDelete("Удаление Актива")] 
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetDetailedResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsset(Guid id, CancellationToken cancellationToken)
        {
            var asset = await assetApplicationService.GetAssetByIdAsync(id, cancellationToken);
            if (asset is null)
                return NotFound($"Актив с Id:{id} не найден");

            var isAssetDel = await assetApplicationService.DeleteModelAsync(id, cancellationToken);
            if (isAssetDel == false)
                return BadRequest($"Невозможно удалить актив");
            return Ok(mapper.Map<AssetDetailedResponce>(asset));
        }

        // [HttpPost]
        // public async Task<IActionResult> 
    }
}
