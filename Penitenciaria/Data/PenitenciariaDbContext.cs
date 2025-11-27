using Microsoft.EntityFrameworkCore;
using Penitenciaria.Modelos;

namespace Penitenciaria.Datos
{
    public class PenitenciariaDbContext : DbContext
    {
        public PenitenciariaDbContext(DbContextOptions<PenitenciariaDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        public DbSet<Celda> Celdas { get; set; }
        public DbSet<Reo> Reos { get; set; }
        public DbSet<Crimen> Crimenes { get; set; }
        public DbSet<ReoCrimen> ReoCrimenes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "Administrador" },
                new Rol { Id = 2, Nombre = "Empleado" }
            );

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario).IsUnique();

            modelBuilder.Entity<Celda>()
                .HasIndex(c => c.NumeroCelda).IsUnique();

            modelBuilder.Entity<Crimen>()
                .HasIndex(c => c.NombreCrimen).IsUnique();

            modelBuilder.Entity<ReoCrimen>()
                .HasIndex(rc => new { rc.ReoID, rc.CrimenID }).IsUnique();
        }
    }
}