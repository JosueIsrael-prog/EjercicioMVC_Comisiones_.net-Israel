namespace Backend.Models;

public class Vendedor
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
}
