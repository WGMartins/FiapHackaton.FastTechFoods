using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCase.Interfaces;
using UseCase.PedidoUseCase.CriarPedido;
using UseCase.PedidoUseCase.Shared;

namespace Api.Controllers.Cliente
{
    [Tags("Cliente")]
    [Authorize(Roles = "Cliente")]
    [Route("cliente/{idCliente:Guid}/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly ICriarPedidoUseCase _criarPedidoUseCase;
        private readonly IAdicionarItemPedidoUseCase _adicionarItemPedidoUseCase;
        private readonly IEnviarPedidoUseCase _enviarPedidoUseCase;
        private readonly ICancelarPedidoUseCase _cancelarPedidoUseCase;

        public PedidoController(ICriarPedidoUseCase criarPedidoUseCase, IAdicionarItemPedidoUseCase adicionarItemPedidoUseCase
            , IEnviarPedidoUseCase enviarPedidoUseCase, ICancelarPedidoUseCase cancelarPedidoUseCase)
        {
            _criarPedidoUseCase = criarPedidoUseCase;
            _adicionarItemPedidoUseCase = adicionarItemPedidoUseCase;
            _enviarPedidoUseCase = enviarPedidoUseCase;
            _cancelarPedidoUseCase = cancelarPedidoUseCase;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Criar(Guid idCliente, AdicionarPedidoDto adicionarPedidoDto)
        {
            try
            {
                return Ok(_criarPedidoUseCase.Criar(idCliente, adicionarPedidoDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}/Item")]
        public IActionResult AdicionarItem(Guid idCliente, Guid id, AdicionarItemPedidoDto adicionarItemDto)
        {
            try
            {
                return Ok(_adicionarItemPedidoUseCase.Adicionar(idCliente, id, adicionarItemDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}/enviar")]
        public IActionResult Enviar(Guid idCliente, Guid id)
        {
            try
            {
                _enviarPedidoUseCase.Enviar(idCliente, id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}/cancelar")]
        public IActionResult Cancelar(Guid idCliente, Guid id)
        {
            try
            {
                _cancelarPedidoUseCase.Cancelar(idCliente, id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
