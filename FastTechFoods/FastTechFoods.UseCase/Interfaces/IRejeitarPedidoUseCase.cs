namespace UseCase.Interfaces
{
    public interface IRejeitarPedidoUseCase
    {
        void Rejeitar (Guid idRestaurante, Guid id);
    }
}
