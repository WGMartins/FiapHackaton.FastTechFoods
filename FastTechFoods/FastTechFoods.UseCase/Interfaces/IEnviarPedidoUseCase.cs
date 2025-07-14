namespace UseCase.Interfaces
{
    public interface IEnviarPedidoUseCase
    {
        void Enviar (Guid idCliente, Guid id);
    }
}
