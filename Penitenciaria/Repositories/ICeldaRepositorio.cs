using Penitenciaria.Modelos;

namespace Penitenciaria.Repositorios
{
    public interface ICeldaRepositorio
    {
        Task<IEnumerable<Celda>> ObtenerTodasAsync();
        Task<Celda?> ObtenerPorIdAsync(int id);
        Task<Celda> CrearAsync(Celda celda);
        Task<bool> ExisteCeldaAsync(string numeroCelda);
        Task EliminarAsync(int id);
        Task ActualizarAsync(Celda celda);
    }
}