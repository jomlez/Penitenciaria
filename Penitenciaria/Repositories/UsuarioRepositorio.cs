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

        // 1. Obtener usuario por Nombre de Usuario
        public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
        {
            return await _contexto.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
        }

        // 2. Obtener usuario por Email
        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await _contexto.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        // 3. Agregar un nuevo usuario
        public async Task<Usuario> AgregarAsync(Usuario usuario)
        {
            usuario.ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(usuario.ContrasenaHash);

            await _contexto.Usuarios.AddAsync(usuario);
            await GuardarCambiosAsync();
            return usuario;
        }

        // 4. Validar contraseña
        public bool ValidarContrasena(Usuario usuario, string contrasenaPlana)
        {
            if (string.IsNullOrEmpty(contrasenaPlana) || string.IsNullOrEmpty(usuario.ContrasenaHash))
            {
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(contrasenaPlana, usuario.ContrasenaHash);
        }

        // 5. Guardar cambios generales
        public async Task GuardarCambiosAsync()
        {
            await _contexto.SaveChangesAsync();
        }

        // 6. Obtener por Token de Refresco
        public async Task<Usuario?> ObtenerPorTokenRefrescoAsync(string tokenRefresco)
        {
            return await _contexto.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.TokenRefresco == tokenRefresco);
        }
    }
}