namespace UseCase.AuthUseCase.AutenticarUsuario;

public class AutenticarUsuarioDto
{
    public string? Email { get; set; }    
    public required string Senha { get; set; }
}
