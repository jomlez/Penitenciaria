using System.ComponentModel.DataAnnotations;

namespace Penitenciaria.Dtos
{
    public class CrearCrimenDto
    {
        [Required]
        public string NombreCrimen { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int PenaMinimaAnios { get; set; }
        public int PenaMaximaAnios { get; set; }
    }
}