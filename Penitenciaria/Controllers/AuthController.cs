using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Penitenciaria.Dtos;
using Penitenciaria.Modelos;
using Penitenciaria.Modelos.Configuraciones;
using Penitenciaria.Repositorios;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Penitenciaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ConfiguracionJwt _configuracionJwt;
        public AuthController(IUsuarioRepositorio usuarioRepositorio, IOptions<ConfiguracionJwt> configuracionJwt)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _configuracionJwt = configuracionJwt.Value;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] RegistroDto modelo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar si el usuario ya existe
            var usuarioExistente = await _usuarioRepositorio.ObtenerPorNombreUsuarioAsync(modelo.NombreUsuario);
            if (usuarioExistente != null)
                return BadRequest("El nombre de usuario ya existe.");

            var emailExistente = await _usuarioRepositorio.ObtenerPorEmailAsync(modelo.Email);
            if (emailExistente != null)
                return BadRequest("El email ya está registrado.");

            // Crear la entidad Usuario
            var nuevoUsuario = new Usuario
            {
                NombreUsuario = modelo.NombreUsuario,
                Nombre = modelo.Nombre,
                Email = modelo.Email,
                ContrasenaHash = modelo.Contrasena,
                RolId = modelo.RolId,
                TokenConfirmacionEmail = Guid.NewGuid().ToString()
            };

            await _usuarioRepositorio.AgregarAsync(nuevoUsuario);

            return Ok(new { mensaje = "Usuario registrado exitosamente" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto modelo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1. Buscar usuario
            var usuario = await _usuarioRepositorio.ObtenerPorNombreUsuarioAsync(modelo.NombreUsuario);

            if (usuario == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            // 2. Validar contraseña
            if (!_usuarioRepositorio.ValidarContrasena(usuario, modelo.Contrasena))
                return Unauthorized("Usuario o contraseña incorrectos.");

            // 3. Generar el Token JWT
            var token = GenerarTokenJwt(usuario);

            return Ok(new
            {
                token = token,
                usuario = usuario.NombreUsuario,
                rol = usuario.Rol?.Nombre
            });
        }

        private string GenerarTokenJwt(Usuario usuario)
        {
            var manejadorToken = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_configuracionJwt.Clave);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            // Añadir Rol si existe
            if (usuario.Rol != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, usuario.Rol.Nombre));
            }

            var descriptorToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_configuracionJwt.DuracionTokenMinutos),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuracionJwt.Emisor,
                Audience = _configuracionJwt.Audiencia
            };

            var token = manejadorToken.CreateToken(descriptorToken);
            return manejadorToken.WriteToken(token);
        }
    }
}