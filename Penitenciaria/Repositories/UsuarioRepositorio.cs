using Microsoft.EntityFrameworkCore;
using Penitenciaria.Datos;
using Penitenciaria.Modelos;
using BCrypt.Net;

namespace Penitenciaria.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly PenitenciariaDbContext _contexto;

        public UsuarioRepositorio(PenitenciariaDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
        {
            return await _contexto.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await _contexto.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> AgregarAsync(Usuario usuario)
        {
            usuario.ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(usuario.ContrasenaHash);

            await _contexto.Usuarios.AddAsync(usuario);
            await GuardarCambiosAsync();
            return usuario;
        }

        public bool ValidarContrasena(Usuario usuario, string contrasenaPlana)
        {
            if (string.IsNullOrEmpty(contrasenaPlana) || string.IsNullOrEmpty(usuario.ContrasenaHash))
            {
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(contrasenaPlana, usuario.ContrasenaHash);
        }

        public async Task GuardarCambiosAsync()
        {
            await _contexto.SaveChangesAsync();
        }

        public async Task<Usuario?> ObtenerPorTokenRefrescoAsync(string tokenRefresco)
        {
            return await _contexto.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.TokenRefresco == tokenRefresco);
        }
    }
}