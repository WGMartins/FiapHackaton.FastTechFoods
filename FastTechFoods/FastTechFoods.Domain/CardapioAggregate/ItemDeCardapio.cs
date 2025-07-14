using System.Runtime.CompilerServices;

namespace Domain.CardapioAggregate
{
    public class ItemDeCardapio : EntityBase
    {
        public Guid CardapioId { get; set; }
        public Cardapio Cardapio { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public TipoRefeicao Tipo { get; set; }

        protected ItemDeCardapio(Guid cardapioId, string nome, decimal valor, string descricao, TipoRefeicao tipo)
        {
            CardapioId = cardapioId;
            Nome = nome;
            Valor = valor;
            Descricao = descricao;
            Tipo = tipo;
        }

        public static ItemDeCardapio Criar(Guid cardapioId, string nome, decimal valor, string descricao, TipoRefeicao tipo)
        {
            return new ItemDeCardapio(cardapioId, nome, valor, descricao, tipo);
        }

        public ItemDeCardapio Atualizar(string nome, decimal valor, string descricao, TipoRefeicao tipo)
        {
            Nome = nome;
            Valor = valor;
            Descricao = descricao;
            Tipo = tipo;

            return this;
        }
    }
}
