using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


namespace OlimpoBlazor.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets para las tablas
      
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Clase> Clases { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Metricas> Metricas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
    }
}
