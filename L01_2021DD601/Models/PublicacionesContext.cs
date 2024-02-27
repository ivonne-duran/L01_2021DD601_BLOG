using Microsoft.EntityFrameworkCore;

namespace L01_2021DD601.Models
{
    public class PublicacionesContext : DbContext
    {
        public PublicacionesContext(DbContextOptions<PublicacionesContext> options) : base(options) { }

        public DbSet<Publicaciones> publicaciones { get; set;}
    }
}
