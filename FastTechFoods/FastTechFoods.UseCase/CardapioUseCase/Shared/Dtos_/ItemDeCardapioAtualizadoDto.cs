using Domain.CardapioAggregate;

namespace UseCase.CardapioUseCase.Shared;

public class ItemDeCardapioAtualizadoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public TipoRefeicao Tipo { get; set; }
}
