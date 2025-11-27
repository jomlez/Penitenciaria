using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Penitenciaria.Modelos
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string NombreUsuario { get; set; } = string.Empty; // UserName

        [Required]
        public string ContrasenaHash { get; set; } = string.Empty; // Password

        public string Email { get; set; } = string.Empty;

        // Campos para JWT y Seguridad (Solicitados en el Word)
        public string? TokenConfirmacionEmail { get; set; }
        public string? TokenRefresco { get; set; } // RefreshToken
        public DateTime? ExpiracionTokenRefresco { get; set; } // RefreshTokenExpiryTime
        public string? TokenReinicioContrasena { get; set; }
        public DateTime? ExpiracionTokenReinicio { get; set; }

        // Relación con Rol
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public virtual Rol? Rol { get; set; }
    }
}