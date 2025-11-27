using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Penitenciaria.Modelos
{
    public class ReoCrimen
    {
        [Key]
        public int ReoCrimenID { get; set; }

        [Required]
        public int ReoID { get; set; }

        [ForeignKey("ReoID")]
        public virtual Reo? Reo { get; set; } 
        [Required]
        public int CrimenID { get; set; }

        [ForeignKey("CrimenID")]
        public virtual Crimen? Crimen { get; set; } 
        public DateTime? FechaComision { get; set; }
    }
}