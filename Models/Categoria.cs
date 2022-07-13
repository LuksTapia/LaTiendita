using System.ComponentModel.DataAnnotations;

namespace LaTiendita.Models
{
    public class Categoria
    {
        public int Id { get; set; }
       
        [Required]
        public string Nombre { get; set; }
        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}
