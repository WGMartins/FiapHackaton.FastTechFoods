namespace Domain.CardapioAggregate
{
    public class Cardapio : EntityBase
    {
        public Guid RestauranteId { get; set; }
        public required Restaurante Restaurante { get; set; }
        public required IList<ItemDeCardapio> ItensDeCardapio { get; set; } = [];

        public ItemDeCardapio AdicionarItem(Guid cardapioId, string nome, decimal valor, string descricao, TipoRefeicao tipo)
        {
            var itemDeCardapio = ItemDeCardapio.Criar(cardapioId, nome, valor, descricao, tipo);

            ItensDeCardapio.Add(itemDeCardapio);

            return itemDeCardapio;
        }

        public void AtualizarItem(Guid id, string nome, decimal valor, string descricao, TipoRefeicao tipo)
        {
            var itemDeCardapio = ItensDeCardapio.Where(x => x.Id == id).First();

            itemDeCardapio.Atualizar(nome, valor, descricao, tipo);
        }

        public void RemoverItem(ItemDeCardapio itemDeCardapio)
        {
            ItensDeCardapio.Remove(itemDeCardapio);
        }
    }
}
