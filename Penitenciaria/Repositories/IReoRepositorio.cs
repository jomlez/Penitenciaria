using Penitenciaria.Modelos;
namespace Penitenciaria.Repositorios
{
    public interface IReoRepositorio
    {
        Task<IEnumerable<Reo>> ObtenerTodosAsync();
        Task CrearReoConCrimenesAsync(Reo reo, List<int> crimenIds);
    }
}