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
        public string NombreUsuario { get; set; } = string.Empty; 

        [Required]
        public string ContrasenaHash { get; set; } = string.Empty; 

        public string Email { get; set; } = string.Empty;

        public string? TokenConfirmacionEmail { get; set; }
        public string? TokenRefresco { get; set; }
        public DateTime? ExpiracionTokenRefresco { get; set; }
        public string? TokenReinicioContrasena { get; set; }
        public DateTime? ExpiracionTokenReinicio { get; set; }

        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public virtual Rol? Rol { get; set; }
    }
}