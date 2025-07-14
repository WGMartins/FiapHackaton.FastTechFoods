using Domain.Interfaces;
using Domain.UsuarioAggregate;
using FluentValidation;
using UseCase.Interfaces;

namespace UseCase.AuthUseCase.AutenticarUsuario;

public class AutenticarUsuarioUseCase : IAutenticarUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IValidator<AutenticarUsuarioDto> _validator;

    public AutenticarUsuarioUseCase(IUsuarioRepository usuarioRepository, IValidator<AutenticarUsuarioDto> validator)
    {
        _usuarioRepository = usuarioRepository;
        _validator = validator;
    }

    public async Task<Usuario?> Autenticar(AutenticarUsuarioDto autenticarUsuarioDto)
    {
        var validacao = _validator.Validate(autenticarUsuarioDto);
        if (!validacao.IsValid)
        {
            string mensagemValidacao = string.Empty;
            foreach (var item in validacao.Errors)
            {
                mensagemValidacao = string.Concat(mensagemValidacao, item.ErrorMessage, ", ");
            }

            throw new Exception(mensagemValidacao.Remove(mensagemValidacao.Length - 2));
        }

        Usuario? usuario = null;

        if (!string.IsNullOrEmpty(autenticarUsuarioDto.Email))
        {
            usuario = await _usuarioRepository.BuscarPorEmailAsync(autenticarUsuarioDto.Email, "Gerente");
            if (usuario != null && VerificarSenha(autenticarUsuarioDto.Senha, usuario.SenhaHash))
                return usuario;
        }

        usuario = await _usuarioRepository.BuscarClientePorCpfOuEmailAsync(autenticarUsuarioDto.Cpf, autenticarUsuarioDto.Email);
        if (usuario != null && VerificarSenha(autenticarUsuarioDto.Senha, usuario.SenhaHash))
            return usuario;

        return null;
    }

    private bool VerificarSenha(string senha, string hash)
    {        
        return BCrypt.Net.BCrypt.Verify(senha, hash);
    }
    
}
