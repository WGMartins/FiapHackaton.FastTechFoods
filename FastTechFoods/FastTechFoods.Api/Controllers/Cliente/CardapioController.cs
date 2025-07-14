using Domain.CardapioAggregate;
using Microsoft.AspNetCore.Mvc;
using UseCase.Interfaces;

namespace Api.Controllers.Cliente
{
    [Tags("Cliente")]
    [Route("cliente/{idCliente:Guid}/[controller]")]
    [ApiController]
    public class CardapioController : ControllerBase
    {
        private readonly IListarItensCardapioUseCase _listarItensCardapioUseCase;

        public CardapioController(IListarItensCardapioUseCase listarItensCardapioUseCase)
        {
            _listarItensCardapioUseCase = listarItensCardapioUseCase;
        }

        [HttpGet]
        [Route("{idCardapio:Guid}/Itens")]
        public IActionResult ListarItens(Guid idCliente, Guid idCardapio, [FromQuery] string? nome, [FromQuery] TipoRefeicao? tipoRefeicao)
        {
            try
            {
                return Ok(_listarItensCardapioUseCase.Listar(idCliente, idCardapio, nome, tipoRefeicao));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
