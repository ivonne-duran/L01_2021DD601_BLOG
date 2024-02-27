using Microsoft.EntityFrameworkCore;


namespace L01_2021DD601.Models
{
    public class UsuariosContext : DbContext
    {
        public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options) { }

        public DbSet<Usuarios> usuarios { get; set; }
    }
}
