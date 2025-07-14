using Domain.CardapioAggregate;

namespace UseCase.CardapioUseCase.AtualizarItemCardapio
{
    public class AtualizarItemCardapioDto
    {
        public required string Nome { get; set; }
        public decimal Valor { get; set; }
        public required string Descricao { get; set; }
        public TipoRefeicao Tipo { get; set; }
    }
}
