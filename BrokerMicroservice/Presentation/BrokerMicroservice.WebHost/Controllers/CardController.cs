using AutoMapper;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.WebHost.Responces.Card;
using Microsoft.AspNetCore.Mvc;

namespace BrokerMicroservice.WebHost.Controllers
{
    [ApiController]
    [Route("api/Card/[controller]")]
    public class CardController(ICardApplicationService cardApplicationService, IMapper mapper) : ControllerBase
    {
        [HttpGet("Вывод Всех Карт")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CardShortResponce>))]
        public async Task<IActionResult> GetAllCard(CancellationToken cancellationToken)
        {
            var card = await cardApplicationService.GetCardsAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<CardShortResponce>>(card));
        }

        // ShortResponce - для получения краткой информации об сущност.(пример - при возврате списка объектов), DetailedResponce - для получения полной информации об объекте (пример - при выводе информации от idНомеру)?
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardDetailedResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetCardById(Guid id, CancellationToken cancellationToken)
        {
            var card = await cardApplicationService.GetCardByIdAsync(id, cancellationToken);
            if (card is null)
                return NotFound($"Карта с Id: {id} не найден");
            return Ok(mapper.Map<CardDetailedResponce>(card));
        }

    }
}
