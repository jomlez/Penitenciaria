using System.ComponentModel.DataAnnotations;

namespace Penitenciaria.Dtos
{
    public class CrearCeldaDto
    {
        [Required(ErrorMessage = "El número de celda es obligatorio")]
        public string NumeroCelda { get; set; } = string.Empty;

        [Required]
        [Range(1, 50, ErrorMessage = "La capacidad debe ser entre 1 y 50")]
        public int Capacidad { get; set; }
    }

    public class CeldaDto
    {
        public int CeldaID { get; set; }
        public string NumeroCelda { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public int OcupacionActual { get; set; }
    }
}