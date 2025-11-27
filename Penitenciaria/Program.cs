using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Penitenciaria.Datos;
using Penitenciaria.Modelos.Configuraciones; // Asegúrate de que el namespace coincida con tus carpetas
using Penitenciaria.Repositorios;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de Base de Datos (SQL Server)
var connectionString = builder.Configuration.GetConnectionString("ConexionDefault");
builder.Services.AddDbContext<PenitenciariaDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(); 
    }));

// 2. Inyección de Dependencias
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ICeldaRepositorio, CeldaRepositorio>();
builder.Services.AddScoped<ICrimenRepositorio, CrimenRepositorio>();
builder.Services.AddScoped<IReoRepositorio, ReoRepositorio>();

// 3. Configuración de JWT
var seccionJwt = builder.Configuration.GetSection("ConfiguracionJwt");
builder.Services.Configure<ConfiguracionJwt>(seccionJwt);

var configuracionJwt = seccionJwt.Get<ConfiguracionJwt>() ?? throw new InvalidOperationException("Falta la sección 'ConfiguracionJwt' en appsettings.json.");
var llave = Encoding.ASCII.GetBytes(configuracionJwt.Clave);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Pon en true si usas HTTPS en producción
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(llave),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = configuracionJwt.Emisor,
        ValidAudience = configuracionJwt.Audiencia,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Agregar servicios Razor Pages (proyecto Razor Pages)
builder.Services.AddRazorPages();

var app = builder.Build();

// Pipeline de peticiones HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// ... código anterior ...

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();