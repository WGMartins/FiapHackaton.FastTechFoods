using Domain.CardapioAggregate;

namespace UseCase.CardapioUseCase.ListarItensCardapio
{
    public class ItemListadosDto
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public decimal Valor { get; set; }
        public required string Descricao { get; set; }
        public TipoRefeicao Tipo { get; set; }
    }
}
