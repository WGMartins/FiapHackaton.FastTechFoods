using FluentValidation;

namespace UseCase.AuthUseCase.AutenticarUsuario;

public class AutenticarUsuarioValidator : AbstractValidator<AutenticarUsuarioDto>
{
    public AutenticarUsuarioValidator()
    {
        RuleFor(x => x.Email)
              .NotEmpty()
              .WithMessage("Email não pode ser nulo ou vazio")
              .MaximumLength(200)
              .WithMessage("Foi atingido o número máximo de caracteres (200)");

        RuleFor(x => x.Senha)
              .NotEmpty()
              .WithMessage("Senha não pode ser nulo ou vazio")
              .MaximumLength(50)
              .WithMessage("Foi atingido o número máximo de caracteres (50)");
    }
}

