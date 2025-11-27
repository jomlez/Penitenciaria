using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Penitenciaria.Modelos
{
    public class Reo
    {
        [Key]
        public int ReoID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public int SentenciaTotalAnios { get; set; }
        public DateTime FechaIngreso { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Activo";

        public int? CeldaID { get; set; }
        [ForeignKey("CeldaID")]
        public Celda? Celda { get; set; }

        public ICollection<ReoCrimen> ReoCrimenes { get; set; } = new List<ReoCrimen>();
    }
}