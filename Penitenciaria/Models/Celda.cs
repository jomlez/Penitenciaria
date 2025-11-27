using System.ComponentModel.DataAnnotations;

namespace Penitenciaria.Modelos
{
    public class Celda
    {
        [Key]
        public int CeldaID { get; set; }
        public string NumeroCelda { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public int OcupacionActual { get; set; } = 0;

        public ICollection<Reo> Reos { get; set; }
    }
}