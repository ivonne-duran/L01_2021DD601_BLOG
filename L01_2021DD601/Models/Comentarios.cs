using System.ComponentModel.DataAnnotations;

namespace L01_2021DD601.Models
{
    public class Comentarios
    {
        [Key]
        public int comentarioId { get; set; }
        public int publicacionId { get; set; }
        public string comentario { get; set; }
        public int usuarioId { get; set; }
    }
}
