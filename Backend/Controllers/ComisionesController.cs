using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComisionesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ComisionesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetComisiones(DateTime fechaInicio, DateTime fechaFin)
    {
        var ventas = await _context.Ventas
            .Include(v => v.Vendedor)
            .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)
            .ToListAsync();
            
        var reglas = await _context.Reglas.ToListAsync();

        var resultado = ventas
            .GroupBy(v => v.Vendedor)
            .Select(g => {
                var total = g.Sum(v => v.Monto);
                var regla = reglas
                    .Where(r => total >= r.MontoMinimo)
                    .OrderByDescending(r => r.MontoMinimo)
                    .FirstOrDefault();
                    
                return new {
                    Vendedor = g.Key?.Nombre ?? "Desconocido",
                    TotalVentas = total,
                    Comision = regla != null ? total * regla.Porcentaje : 0
                };
            });
            
        return Ok(resultado);
    }
}
