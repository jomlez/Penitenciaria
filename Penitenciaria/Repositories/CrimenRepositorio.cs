using Microsoft.EntityFrameworkCore;
using Penitenciaria.Datos;
using Penitenciaria.Modelos;

namespace Penitenciaria.Repositorios
{
    public class CrimenRepositorio : ICrimenRepositorio
    {
        private readonly PenitenciariaDbContext _contexto;
        public CrimenRepositorio(PenitenciariaDbContext contexto) => _contexto = contexto;

        public async Task<IEnumerable<Crimen>> ObtenerTodosAsync() => await _contexto.Crimenes.ToListAsync();

        public async Task<Crimen> CrearAsync(Crimen crimen)
        {
            await _contexto.Crimenes.AddAsync(crimen);
            await _contexto.SaveChangesAsync();
            return crimen;
        }
    }
}