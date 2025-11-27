using Penitenciaria.Modelos;
namespace Penitenciaria.Repositorios
{
    public interface ICrimenRepositorio
    {
        Task<IEnumerable<Crimen>> ObtenerTodosAsync();
        Task<Crimen> CrearAsync(Crimen crimen);
    }
}