using Domain.CardapioAggregate;

namespace UseCase.CardapioUseCase.AdicionarItemCardapio
{
    public class AdicionarItemCardapioDto
    {
        public required string Nome { get; set; }
        public decimal Valor { get; set; }
        public required string Descricao { get; set; }
        public TipoRefeicao Tipo { get; set; }
    }
}
