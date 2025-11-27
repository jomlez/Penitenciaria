using Microsoft.EntityFrameworkCore;
using Penitenciaria.Datos;
using Penitenciaria.Modelos;

namespace Penitenciaria.Repositorios
{
    public class ReoRepositorio : IReoRepositorio
    {
        private readonly PenitenciariaDbContext _contexto;
        public ReoRepositorio(PenitenciariaDbContext contexto) => _contexto = contexto;

        public async Task<IEnumerable<Reo>> ObtenerTodosAsync()
        {
            return await _contexto.Reos
                .Include(r => r.Celda)
                .ToListAsync();
        }

        public async Task CrearReoConCrimenesAsync(Reo reo, List<int> crimenIds)
        {

            var strategy = _contexto.Database.CreateExecutionStrategy();


            await strategy.ExecuteAsync(async () =>
            {

                using var transaccion = await _contexto.Database.BeginTransactionAsync();
                try
                {

                    await _contexto.Reos.AddAsync(reo);
                    await _contexto.SaveChangesAsync(); 
                    if (crimenIds != null && crimenIds.Any())
                    {
                        foreach (var crimenId in crimenIds)
                        {
                            var reoCrimen = new ReoCrimen
                            {
                                ReoID = reo.ReoID,
                                CrimenID = crimenId,
                                FechaComision = DateTime.Now
                            };
                            await _contexto.ReoCrimenes.AddAsync(reoCrimen);
                        }
                    }

                    var celda = await _contexto.Celdas.FindAsync(reo.CeldaID);
                    if (celda != null)
                    {
                        celda.OcupacionActual += 1;
                    }

                    await _contexto.SaveChangesAsync();

                    await transaccion.CommitAsync();
                }
                catch
                {
                    await transaccion.RollbackAsync();
                    throw;
                }
            });
        }
    }
}