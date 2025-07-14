namespace UseCase.Interfaces
{
    public interface ICancelarPedidoUseCase
    {
        void Cancelar (Guid idCliente, Guid id);
    }
}
