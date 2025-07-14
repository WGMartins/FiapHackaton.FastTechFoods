using Domain.UsuarioAggregate;
using UseCase.AuthUseCase.AutenticarUsuario;

namespace UseCase.Interfaces;

public interface IAutenticarUsuarioUseCase
{
    Task<Usuario?> Autenticar(AutenticarUsuarioDto autenticarUsuarioDto);
}
