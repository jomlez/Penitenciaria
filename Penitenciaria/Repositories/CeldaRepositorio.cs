using Microsoft.EntityFrameworkCore;
using Penitenciaria.Datos;
using Penitenciaria.Modelos;

namespace Penitenciaria.Repositorios
{
    public class CeldaRepositorio : ICeldaRepositorio
    {
        private readonly PenitenciariaDbContext _contexto;

        public CeldaRepositorio(PenitenciariaDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Celda>> ObtenerTodasAsync()
        {
            return await _contexto.Celdas.ToListAsync();
        }

        public async Task<Celda?> ObtenerPorIdAsync(int id)
        {
            return await _contexto.Celdas.FindAsync(id);
        }

        public async Task<bool> ExisteCeldaAsync(string numeroCelda)
        {
            return await _contexto.Celdas.AnyAsync(c => c.NumeroCelda == numeroCelda);
        }

        public async Task<Celda> CrearAsync(Celda celda)
        {
            await _contexto.Celdas.AddAsync(celda);
            await _contexto.SaveChangesAsync();
            return celda;
        }
    }
}