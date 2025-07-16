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

        public async Task<Usuario?> BuscarPorEmailAsync(string email)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.Role != "Cliente" && u.Email == email);            
        }
    }
}
