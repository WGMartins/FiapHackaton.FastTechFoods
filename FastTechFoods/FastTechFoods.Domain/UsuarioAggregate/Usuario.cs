namespace Domain.UsuarioAggregate
{
    public class Usuario : EntityBase
    {        
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public required string SenhaHash { get; set; }
        public required string Role { get; set; }

    }
}
