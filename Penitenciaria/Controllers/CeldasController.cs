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
    public class CeldasController : ControllerBase
    {
        private readonly ICeldaRepositorio _celdaRepositorio;

        public CeldasController(ICeldaRepositorio celdaRepositorio)
        {
            _celdaRepositorio = celdaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCeldas()
        {
            var celdas = await _celdaRepositorio.ObtenerTodasAsync();
            return Ok(celdas);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCelda([FromBody] CrearCeldaDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _celdaRepositorio.ExisteCeldaAsync(modelo.NumeroCelda))
            {
                return BadRequest("Ya existe una celda con ese número.");
            }

            var nuevaCelda = new Celda
            {
                NumeroCelda = modelo.NumeroCelda,
                Capacidad = modelo.Capacidad,
                OcupacionActual = 0
            };

            await _celdaRepositorio.CrearAsync(nuevaCelda);

            return Ok(new { mensaje = "Celda creada exitosamente" });
        }
    }
}