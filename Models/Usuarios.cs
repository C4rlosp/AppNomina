using System.ComponentModel.DataAnnotations;

namespace AppNomina.Models
{
    public class Usuarios
    {
        [Key]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string NombreCompleto { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public DateTime FechaRegistro { get; set; }

        public char Estado {  get; set; }
    }
}
