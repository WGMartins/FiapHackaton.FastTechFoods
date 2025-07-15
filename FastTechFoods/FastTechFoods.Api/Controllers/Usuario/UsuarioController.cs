using Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;
using UseCase.AuthUseCase.AutenticarUsuario;
using UseCase.Interfaces;

namespace Api.Controllers.Usuario
{
    [Tags("Autenticação")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IAutenticarUsuarioUseCase _autenticarUsuarioUseCase;
        private readonly JwtToken _jwtToken;

        private readonly IMessagePublisher _publisherA;
        private readonly IMessagePublisher _publisherB;

        public UsuarioController(IAutenticarUsuarioUseCase autenticarUsuarioUseCase, JwtToken jwtToken, Func<string, IMessagePublisher> publisherFactory)
        {
            _autenticarUsuarioUseCase = autenticarUsuarioUseCase;
            _jwtToken = jwtToken;

            _publisherA = publisherFactory("ProducerPedido");
            _publisherB = publisherFactory("ProducerCardapio");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AutenticarUsuarioDto autenticarUsuarioDto)
        {

            try
            {
                var usuario = await _autenticarUsuarioUseCase.Autenticar(autenticarUsuarioDto);
                if (usuario == null) return Unauthorized();

                var token = _jwtToken.GerarToken(usuario);
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            
        }
    }
}
