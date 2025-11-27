using System.ComponentModel.DataAnnotations;
using Penitenciaria.Modelos;

namespace Penitenciaria.Modelos
{
    public class Crimen
    {
        [Key]
        public int CrimenID { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreCrimen { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public int? PenaMinimaAnios { get; set; }
        public int? PenaMaximaAnios { get; set; }

        // Relación inversa para saber cuántos reos cometieron este crimen
        public ICollection<ReoCrimen> ReoCrimenes { get; set; } = new List<ReoCrimen>();
    }
}