using System.ComponentModel.DataAnnotations;

namespace Penitenciaria.Dtos
{
    public class CrearReoDto
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public string Apellido { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public int SentenciaTotalAnios { get; set; }

        [Required]
        public int CeldaID { get; set; }

        public List<int> CrimenIds { get; set; } = new List<int>();
    }

    public class ReoDto
    {
        public int ReoID { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Celda { get; set; } = string.Empty;
        public int Sentencia { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}