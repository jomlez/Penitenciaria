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
            // 1. Obtenemos la estrategia de ejecución (Obligatorio por EnableRetryOnFailure)
            var strategy = _contexto.Database.CreateExecutionStrategy();

            // 2. Ejecutamos la transacción DENTRO de la estrategia
            await strategy.ExecuteAsync(async () =>
            {
                // Aquí empieza la transacción manual como antes
                using var transaccion = await _contexto.Database.BeginTransactionAsync();
                try
                {
                    // A. Guardar al Reo
                    await _contexto.Reos.AddAsync(reo);
                    await _contexto.SaveChangesAsync(); // Aquí se genera el ID del Reo

                    // B. Guardar la relación Reo-Crimen
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

                    // C. Actualizar Ocupación de la Celda
                    var celda = await _contexto.Celdas.FindAsync(reo.CeldaID);
                    if (celda != null)
                    {
                        celda.OcupacionActual += 1;
                    }

                    await _contexto.SaveChangesAsync();

                    // Confirmamos la transacción
                    await transaccion.CommitAsync();
                }
                catch
                {
                    // Si algo falla, deshacemos todo
                    await transaccion.RollbackAsync();
                    throw; // Importante: avisar al controlador que falló
                }
            });
        }
    }
}