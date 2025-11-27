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
    public class ReosController : ControllerBase
    {
        private readonly IReoRepositorio _reoRepo;
        private readonly ICeldaRepositorio _celdaRepo;

        public ReosController(IReoRepositorio reoRepo, ICeldaRepositorio celdaRepo)
        {
            _reoRepo = reoRepo;
            _celdaRepo = celdaRepo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerReos()
        {
            var reos = await _reoRepo.ObtenerTodosAsync();
            var dtos = reos.Select(r => new ReoDto
            {
                ReoID = r.ReoID,
                NombreCompleto = $"{r.Nombre} {r.Apellido}",
                Celda = r.Celda?.NumeroCelda ?? "Sin celda",
                Sentencia = r.SentenciaTotalAnios,
                Estado = r.Estado
            });
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> CrearReo([FromBody] CrearReoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Validar si la celda existe y tiene espacio
            var celda = await _celdaRepo.ObtenerPorIdAsync(dto.CeldaID);
            if (celda == null) return BadRequest("La celda no existe.");
            if (celda.OcupacionActual >= celda.Capacidad) return BadRequest("La celda está llena.");

            var nuevoReo = new Reo
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento,
                SentenciaTotalAnios = dto.SentenciaTotalAnios,
                CeldaID = dto.CeldaID,
                Estado = "Activo"
            };

            await _reoRepo.CrearReoConCrimenesAsync(nuevoReo, dto.CrimenIds);
            return Ok(new { mensaje = "Reo ingresado exitosamente" });
        }
    }
}