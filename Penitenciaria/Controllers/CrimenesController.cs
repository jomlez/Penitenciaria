using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penitenciaria.Dtos;
using Penitenciaria.Modelos;
using Penitenciaria.Repositorios;

namespace Penitenciaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CrimenesController : ControllerBase
    {
        private readonly ICrimenRepositorio _repo;
        public CrimenesController(ICrimenRepositorio repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() => Ok(await _repo.ObtenerTodosAsync());

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearCrimenDto dto)
        {
            var crimen = new Crimen
            {
                NombreCrimen = dto.NombreCrimen,
                Descripcion = dto.Descripcion,
                PenaMinimaAnios = dto.PenaMinimaAnios,
                PenaMaximaAnios = dto.PenaMaximaAnios
            };
            await _repo.CrearAsync(crimen);
            return Ok(new { mensaje = "Crimen agregado" });
        }
    }
}