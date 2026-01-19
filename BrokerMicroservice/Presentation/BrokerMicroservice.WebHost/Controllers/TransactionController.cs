using AutoMapper;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.WebHost.Responces.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace BrokerMicroservice.WebHost.Controllers
{
    [ApiController]
    [Route("api/Transaction/[controller]")]
    public class TransactionController(ITransactionApplicationService transactionApplicationService, IMapper mapper) : ControllerBase
    {
        [HttpGet("Вывод Всех Транзакций")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransactionShortResponce>))]
        public async Task<IActionResult> GetTransactionsAsync(CancellationToken cancellationToken)
        {
            var transaction = await transactionApplicationService.GetTransactionsAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<TransactionShortResponce>>(transaction));
        }

        // ShortResponce - для получения краткой информации об сущност.(пример - при возврате списка объектов), DetailedResponce - для получения полной информации об объекте (пример - при выводе информации от idНомеру)?
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionDetailedResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetTransactionById(Guid id, CancellationToken cancellationToken)
        {
            var transaction = await transactionApplicationService.GetTransactionByIdAsync(id, cancellationToken);
            if (transaction is null)
                return NotFound($"Транзакции с Id: {id} не найден");
            return Ok(mapper.Map<TransactionDetailedResponce>(transaction));
        }
    }
}
