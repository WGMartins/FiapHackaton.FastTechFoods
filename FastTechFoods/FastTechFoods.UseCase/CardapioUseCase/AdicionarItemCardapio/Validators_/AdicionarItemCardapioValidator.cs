using FluentValidation;

namespace UseCase.CardapioUseCase.AdicionarItemCardapio
{
    public class AdicionarItemCardapioValidator : AbstractValidator<AdicionarItemCardapioDto>
    {
        public AdicionarItemCardapioValidator()
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
