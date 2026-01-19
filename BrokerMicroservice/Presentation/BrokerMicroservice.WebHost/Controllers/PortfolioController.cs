using AutoMapper;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.WebHost.Responces.Portfolio;
using Microsoft.AspNetCore.Mvc;

namespace BrokerMicroservice.WebHost.Controllers
{
    [ApiController]
    [Route("api/Portfolio/[controller]")]
    public class PortfolioController(IPortfolioApplicationService portfolioApplicationService, IMapper mapper) : ControllerBase
    {
        [HttpGet("Вывод Всех Портфелей")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PortfolioShortResponce>))]
        public async Task<IActionResult> GetAllPortfolio(CancellationToken cancellationToken)
        {
            var portfolio = await portfolioApplicationService.GetPortfolioAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<PortfolioShortResponce>>(portfolio));
        }

        // ShortResponce - для получения краткой информации об сущност.(пример - при возврате списка объектов), DetailedResponce - для получения полной информации об объекте (пример - при выводе информации от idНомеру)?
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortfolioDetailedResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetPortfolioById(Guid id, CancellationToken cancellationToken)
        {
            var portfolio = await portfolioApplicationService.GetPortfolioByIdAsync(id, cancellationToken);
            if (portfolio is null)
                return NotFound($"Портфель с Id: {id} не найден");
            return Ok(mapper.Map<PortfolioDetailedResponce>(portfolio));
        } 

    }
}
