using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCase.Interfaces;

namespace Api.Controllers.Restaurante
{
    [Tags("Restaurante - Atendente")]
    [Authorize(Roles = "Atendente")]
    [Route("restaurante/{idRestaurante:Guid}/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IListarPedidosUseCase _listarPedidosUseCase;
        private readonly IAceitarPedidoUseCase _aceitarPedidoUseCase;
        private readonly IRejeitarPedidoUseCase _rejeitarPedidoUseCase;

        public PedidoController(IListarPedidosUseCase listarPedidosUseCase, IAceitarPedidoUseCase aceitarPedidoUseCase, IRejeitarPedidoUseCase rejeitarPedidoUseCase)
        {
            _listarPedidosUseCase = listarPedidosUseCase;
            _aceitarPedidoUseCase = aceitarPedidoUseCase;
            _rejeitarPedidoUseCase = rejeitarPedidoUseCase;
        }

        [HttpGet]
        public IActionResult Listar(Guid idRestaurante)
        {
            try
            {
                return Ok(_listarPedidosUseCase.Listar(idRestaurante));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}/aceitar")]
        public IActionResult Aceitar(Guid idRestaurante, Guid id)
        {
            try
            {
                _aceitarPedidoUseCase.Aceitar(idRestaurante, id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}/rejeitar")]
        public IActionResult Rejeitar(Guid idRestaurante, Guid id)
        {
            try
            {
                _rejeitarPedidoUseCase.Rejeitar(idRestaurante, id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
