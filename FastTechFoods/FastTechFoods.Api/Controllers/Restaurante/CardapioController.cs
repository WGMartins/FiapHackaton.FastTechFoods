using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCase.CardapioUseCase.AdicionarItemCardapio;
using UseCase.CardapioUseCase.AtualizarItemCardapio;
using UseCase.Interfaces;

namespace Api.Controllers.Restaurante
{
    [Tags("Restaurante - Gerente")]
    [Authorize(Roles = "Gerente")]
    [Route("restaurante/{idRestaurante:Guid}/[controller]")]
    [ApiController]
    public class CardapioController : ControllerBase
    {
        private readonly IAdicionarItemCardapioUseCase _adicionarItemCardapioUseCase;
        private readonly IAtualizarItemCardapioUseCase _atualizarItemCardapioUseCase;
        private readonly IRemoverItemCardapioUseCase _removerItemCardapioUseCase;

        public CardapioController(IAdicionarItemCardapioUseCase adicionarItemCardapioUseCase, IAtualizarItemCardapioUseCase atualizarItemCardapioUseCase, IRemoverItemCardapioUseCase removerItemCardapioUseCase)
        {
            _adicionarItemCardapioUseCase = adicionarItemCardapioUseCase;
            _atualizarItemCardapioUseCase = atualizarItemCardapioUseCase;
            _removerItemCardapioUseCase = removerItemCardapioUseCase;
        }

        [HttpPost]
        [Route("{idCardapio:Guid}/Item")]
        public IActionResult Adicionar(Guid idRestaurante, Guid idCardapio, AdicionarItemCardapioDto adicionarItemDto)
        {
            try
            {
                return Ok(_adicionarItemCardapioUseCase.Adicionar(idRestaurante, idCardapio, adicionarItemDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{idCardapio:Guid}/Item/{id:Guid}")]
        public IActionResult Atualizar(Guid idRestaurante, Guid idCardapio, Guid id, AtualizarItemCardapioDto atualizarItemDto)
        {
            try
            {
                _atualizarItemCardapioUseCase.Atualizar(idRestaurante, idCardapio, id, atualizarItemDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{idCardapio:Guid}/Item/{id:Guid}")]
        public IActionResult Remover(Guid idRestaurante, Guid idCardapio, Guid id)
        {
            try
            {
                _removerItemCardapioUseCase.Remover(idRestaurante, idCardapio, id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
