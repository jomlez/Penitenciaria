using Penitenciaria.Models;
using System.ComponentModel.DataAnnotations;

namespace Penitenciaria.Modelos
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty; // Ej: Admin, Empleado
        public ICollection<Usuario> Usuario { get; set; } // Relación 1:N
    }
}