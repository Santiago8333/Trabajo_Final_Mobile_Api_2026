using Microsoft.EntityFrameworkCore;

namespace Trabajo_Final_Mobile_Api_2026.Models
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set; }
        public DbSet<Reparacion> Reparacion { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<DetalleReparacion> DetalleReparacion { get; set; }


    }




}