using FluentValidation;

namespace UseCase.CardapioUseCase.AtualizarItemCardapio
{
    public class AtualizarItemValidator : AbstractValidator<AtualizarItemCardapioDto>
    {
        public AtualizarItemValidator()
        {
            RuleFor(x => x.Nome)
               .NotEmpty()
               .WithMessage("Nome não pode ser nulo ou vazio")
               .MaximumLength(100)
               .WithMessage("Foi atingido o número máximo de caracteres (100)");

            RuleFor(x => x.Descricao)
               .NotEmpty()
               .WithMessage("Descricao não pode ser nulo ou vazio")
               .MaximumLength(100)
               .WithMessage("Foi atingido o número máximo de caracteres (100)");
        }
    }
}
