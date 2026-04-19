using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Regla> Reglas { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }
    public DbSet<Venta> Ventas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapeo explicito de tablas para que en Supabase se llamen igual que las clases
        modelBuilder.Entity<Regla>().ToTable("Regla");
        modelBuilder.Entity<Vendedor>().ToTable("Vendedor");
        modelBuilder.Entity<Venta>().ToTable("Venta");

        // Relación Vendedor -> Venta
        modelBuilder.Entity<Venta>()
            .HasOne(v => v.Vendedor)
            .WithMany(v => v.Ventas)
            .HasForeignKey(v => v.VendedorId);

        // Seeding Data
        modelBuilder.Entity<Regla>().HasData(
            new Regla { Id = 1, Porcentaje = 0.06m, MontoMinimo = 0.00m },
            new Regla { Id = 2, Porcentaje = 0.08m, MontoMinimo = 500.00m },
            new Regla { Id = 3, Porcentaje = 0.10m, MontoMinimo = 800.00m },
            new Regla { Id = 4, Porcentaje = 0.15m, MontoMinimo = 1000.00m }
        );

        modelBuilder.Entity<Vendedor>().HasData(
            new Vendedor { Id = 1, Nombre = "Juan Pérez" },
            new Vendedor { Id = 2, Nombre = "María García" },
            new Vendedor { Id = 3, Nombre = "Carlos López" }
        );

        modelBuilder.Entity<Venta>().HasData(
            // Juan Perez suma más de 1000
            new Venta { Id = 1, VendedorId = 1, Monto = 500.00m, FechaVenta = new DateTime(2025, 5, 10, 0, 0, 0, DateTimeKind.Utc) },
            new Venta { Id = 2, VendedorId = 1, Monto = 600.00m, FechaVenta = new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Utc) },
            // Maria Garcia
            new Venta { Id = 3, VendedorId = 2, Monto = 600.00m, FechaVenta = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc) },
            // Carlos Lopez
            new Venta { Id = 4, VendedorId = 3, Monto = 200.00m, FechaVenta = new DateTime(2026, 5, 20, 0, 0, 0, DateTimeKind.Utc) },
            new Venta { Id = 5, VendedorId = 3, Monto = 100.00m, FechaVenta = new DateTime(2026, 6, 2, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
