using Domain.Interfaces;
using Domain.UsuarioAggregate;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> BuscarClientePorCpfOuEmailAsync(string? cpf, string? email)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u =>
            u.Role == "Cliente" &&
            (
                (!string.IsNullOrEmpty(email) && u.Email == email) ||
                (!string.IsNullOrEmpty(cpf) && u.Cpf == cpf)
            ));
        }

        public async Task<Usuario?> BuscarPorEmailAsync(string email, string role)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.Role == role && u.Email == email);            
        }
    }
}
