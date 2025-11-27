using Penitenciaria.Modelos;

namespace Penitenciaria.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
        Task<Usuario?> ObtenerPorEmailAsync(string email);
        Task<Usuario> AgregarAsync(Usuario usuario);
        bool ValidarContrasena(Usuario usuario, string contrasena);
        Task GuardarCambiosAsync();
        Task<Usuario?> ObtenerPorTokenRefrescoAsync(string tokenRefresco);
    }
}