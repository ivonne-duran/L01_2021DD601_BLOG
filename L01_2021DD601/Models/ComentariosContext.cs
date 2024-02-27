using Microsoft.EntityFrameworkCore;

namespace L01_2021DD601.Models
{
    public class ComentariosContext : DbContext
    {
        public ComentariosContext(DbContextOptions<ComentariosContext> options) : base(options) { }

        public DbSet<Comentarios> comentarios { get; set; }
    }
}
