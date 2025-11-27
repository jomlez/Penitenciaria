using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Penitenciaria.Modelos
{
    public class ReoCrimen
    {
        [Key]
        public int ReoCrimenID { get; set; }

        // Clave Foránea hacia Reo
        [Required]
        public int ReoID { get; set; }

        [ForeignKey("ReoID")]
        public virtual Reo? Reo { get; set; } // Propiedad de navegación opcional

        // Clave Foránea hacia Crimen
        [Required]
        public int CrimenID { get; set; }

        [ForeignKey("CrimenID")]
        public virtual Crimen? Crimen { get; set; } // Propiedad de navegación opcional

        // Dato extra específico de esta relación
        public DateTime? FechaComision { get; set; }
    }
}