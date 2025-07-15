using Domain.UsuarioAggregate;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> BuscarPorEmailAsync(string email, string role);
        Task<Usuario?> BuscarClientePorCpfOuEmailAsync(string? cpf, string? email);
    }
}
