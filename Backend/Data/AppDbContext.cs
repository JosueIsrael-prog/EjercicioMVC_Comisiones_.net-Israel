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
    }
}
