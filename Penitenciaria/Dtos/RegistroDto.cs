using System.ComponentModel.DataAnnotations;

namespace Penitenciaria.Dtos
{
    public class RegistroDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Contrasena { get; set; } = string.Empty;
        public int RolId { get; set; } = 2;
    }
}