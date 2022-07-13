
using System.ComponentModel.DataAnnotations;

namespace LaTiendita.Models
{
    public class Usuario
    {
       
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Nombre { get; set; }
        public DateTime FechaDeCreacion { get; set; } = DateTime.Now;
    }
}
