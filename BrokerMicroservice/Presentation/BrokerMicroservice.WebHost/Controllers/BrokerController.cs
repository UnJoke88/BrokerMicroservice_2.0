using AutoMapper;
using BrokerMicroservice.Application.Models.Broker;
using BrokerMicroservice.Application.Services;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.Application.Services.Abstractions.Base;
using BrokerMicroservice.Domain.Entities;
using BrokerMicroservice.WebHost.Requests.Broker;
using BrokerMicroservice.WebHost.Responces.Broker;
using Microsoft.AspNetCore.Mvc;


namespace BrokerMicroservice.WebHost.Controllers
{
    [ApiController]
    [Route("api/Broker/[controller]")]
    public class BrokerController(IBrokerApplicationService brokerApplicationService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrokerShortResponce>))]
        public async Task<IActionResult> GetAllBroker(CancellationToken cancellationToken)
        {
            var broker = await brokerApplicationService.GetBrokerAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<BrokerShortResponce>>(broker));
        }

        // ShortResponce - для получения краткой информации об сущност.(пример - при возврате списка объектов), DetailedResponce - для получения полной информации об объекте (пример - при выводе информации от idНомеру)?
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrokerDetailedResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetBrokerById(Guid id, CancellationToken cancellationToken)
        {
            var broker = await brokerApplicationService.GetBrokerByIdAsync(id, cancellationToken);
            if (broker is null)
                return NotFound($"Broker with id:{id} not found");
            return Ok(mapper.Map<BrokerDetailedResponce>(broker));
        }

        [HttpPost] // Запись в Базу Данных 
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BrokerShortResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CreateBroker(CreateBrokerRequest request, CancellationToken cancellationToken)
        {
            var broker = mapper.Map<CreateBrokerModel>(request);
            var createdBroker = await brokerApplicationService.CreateBrokerAsync(broker, cancellationToken);
            if (createdBroker is null)
                return BadRequest($"Broker can not be created");
            var brokerResponce = mapper.Map<BrokerShortResponce>(createdBroker);
            return CreatedAtAction(nameof(GetBrokerById), new { brokerResponce.Id }, brokerResponce);
        }

        [HttpPatch]// Редактирование
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrokerDetailedResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> UpdateBroker(UpdateBrokerRequest request, CancellationToken cancellationToken)
        {
            var broker = await brokerApplicationService.GetBrokerByIdAsync(request.id, cancellationToken);
            if (broker is null)
                return NotFound($"Broker with id:{request.id} not found");

            var newBroker = mapper.Map<BrokerModel>(request);

            var isBrokerUpdated = await brokerApplicationService.UpdateBrokerAsync(newBroker, cancellationToken);
            if (isBrokerUpdated == false)
                return BadRequest($"Broker can not be redact");
           
            var updated = await brokerApplicationService.GetBrokerByIdAsync(request.id, cancellationToken);
            return Ok(mapper.Map<BrokerDetailedResponce>(updated));
        }

        [HttpDelete] // Удалить экземпляр администратора
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrokerDetailedResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteBroker(Guid id, CancellationToken cancellationToken)
        {
            var broker = await brokerApplicationService.GetBrokerByIdAsync(id, cancellationToken);
            if (broker is null)
                return NotFound($"Broker with id:{id} not found");

            var isBrokerDel = await brokerApplicationService.DeleteBrokerAsync(id, cancellationToken);
            if (isBrokerDel == false)
                return BadRequest($"Broker can not be delete");
            return Ok(mapper.Map<BrokerDetailedResponce>(broker));
        }

        // [HttpPost]
        // public async Task<IActionResult> 
    }
}
