using System.ComponentModel.DataAnnotations;

namespace LaTiendita.Models
{
    public class Talle
    {
        public int Id { get; set; }

        [RegularExpression("XS|S|M|L|XL|xs|s|m|l|xl", ErrorMessage = "Talle inválido")]
        public string Nombre { get; set; }
    }
}
