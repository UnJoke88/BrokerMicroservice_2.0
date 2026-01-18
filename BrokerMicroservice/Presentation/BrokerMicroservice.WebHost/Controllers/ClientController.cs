using AutoMapper;
using BrokerMicroservice.Application.Models.Client;
using BrokerMicroservice.Application.Models.Transaction;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.WebHost.Requests.Client;
using BrokerMicroservice.WebHost.Requests.Transaction;
using BrokerMicroservice.WebHost.Responces.Client;
using BrokerMicroservice.WebHost.Responces.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace BrokerMicroservice.WebHost.Controllers
{
    [ApiController]
    [Route("api/Client/[controller]")]
    public class ClientController(IClientApplicationService clientApplicationService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientShortResponce>))]
        public async Task<IActionResult> GetAllClient(CancellationToken cancellationToken)
        {
            var client = await clientApplicationService.GetClientAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<ClientShortResponce>>(client));
        }

        // ShortResponce - для получения краткой информации об сущност.(пример - при возврате списка объектов), DetailedResponce - для получения полной информации об объекте (пример - при выводе информации от idНомеру)?
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDetailedResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetClientById(Guid id, CancellationToken cancellationToken)
        {
            var client = await clientApplicationService.GetClientByIdAsync(id, cancellationToken);
            if (client is null)
                return NotFound($"Клиент с Id: {id} не найден");
            return Ok(mapper.Map<ClientDetailedResponce>(client));
        }

        [HttpPost] // Запись в Базу Данных 
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientShortResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CreateClient(CreateClientRequest request, CancellationToken cancellationToken)
        {
            var client = mapper.Map<CreateClientModel>(request);
            var createdClient = await clientApplicationService.CreateClientAsync(client, cancellationToken);
            if (createdClient is null)
                return BadRequest($"Ошибка в создании клиента");
            var clientResponce = mapper.Map<ClientShortResponce>(createdClient);
            return CreatedAtAction(nameof(GetClientById), new { clientResponce.Id }, clientResponce);
        }

        [HttpPatch]// Редактирование
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDetailedResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> UpdateClient(UpdateClientRequest request, CancellationToken cancellationToken)
        {
            var client = await clientApplicationService.GetClientByIdAsync(request.Id, cancellationToken);
            if (client is null)
                return NotFound($"Клиент с Id:{request.Id} не найден");

            var newClient = mapper.Map<ClientModel>(request);

            var isClientUpdated = await clientApplicationService.UpdateClientAsync(newClient, cancellationToken);
            if (isClientUpdated == false)
                return BadRequest($"Невозможно изменить клиента");

            var updated = await clientApplicationService.GetClientByIdAsync(request.Id, cancellationToken);
            return Ok(mapper.Map<ClientDetailedResponce>(updated));
        }

        [HttpDelete] // Удалить экземпляр администратора
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDetailedResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteClient(Guid id, CancellationToken cancellationToken)
        {
            var client = await clientApplicationService.GetClientByIdAsync(id, cancellationToken);
            if (client is null)
                return NotFound($"Клиент с Id:{id} не найден");

            var isClientDel = await clientApplicationService.DeleteClientAsync(id, cancellationToken);
            if (isClientDel == false)
                return BadRequest($"Невозможно удалить клиента");
            return Ok(mapper.Map<ClientDetailedResponce>(client));
        }

        //Операции с картой
        [HttpPost("buy")]
        public async Task<IActionResult> BuyAsset([FromBody] CreateTransactionRequest request, CancellationToken ct)
        {
            if (request.Type != TransactionType.Purchase)
                return BadRequest("TransactionType должен быть Buy");

            var model = mapper.Map<CreateTransactionModel>(request);
            var result = await clientApplicationService.BuyAssetAsync(model, ct);

            return result is null ? BadRequest("Ошибка покупки актива") : Ok(result);
        }

        [HttpPost("sale")]
        public async Task<IActionResult> MakeSale([FromBody] CreateTransactionRequest request, CancellationToken ct)
        {
            if (request.Type != TransactionType.Sale)
                return BadRequest("TransactionType должен быть Sale");

            var model = mapper.Map<CreateTransactionModel>(request);
            var result = await clientApplicationService.MakeSaleAsync(model, ct);

            return result is null ? BadRequest("Ошибка продажи актива") : Ok(result);
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> MakeDeposit([FromBody] CreateTransactionRequest request, CancellationToken ct)
        {
            if (request.Type != TransactionType.Replenishment)
                return BadRequest("TransactionType должен быть Deposit");

            var model = mapper.Map<CreateTransactionModel>(request);
            var result = await clientApplicationService.MakeDepositAsync(model, ct);

            return result is null ? BadRequest("Ошибка пополнения") : Ok(result);
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> MakeWithdraw([FromBody] CreateTransactionRequest request, CancellationToken ct)
        {
            if (request.Type != TransactionType.Removing)
                return BadRequest("TransactionType должен быть Withdraw");

            var model = mapper.Map<CreateTransactionModel>(request);
            var result = await clientApplicationService.MakeWithdrawAsync(model, ct);

            return result is null ? BadRequest("Ошибка снятия") : Ok(result);

            // [HttpPost]
            // public async Task<IActionResult> 
        }
    }
}
