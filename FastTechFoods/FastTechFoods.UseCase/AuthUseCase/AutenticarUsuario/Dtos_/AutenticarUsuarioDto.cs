namespace UseCase.AuthUseCase.AutenticarUsuario;

public class AutenticarUsuarioDto
{
    public string? Email { get; set; }
    public string? Cpf { get; set; }
    public required string Senha { get; set; }
}
