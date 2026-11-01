using Microsoft.EntityFrameworkCore;
using Trabajo_Final_Mobile_Api_2026.Models;

namespace Trabajo_Final_Mobile_Api_2026.Repositorios;

public interface IDetalleReparacionRepositorio
{
    Task<List<DetalleReparacion>> ObtenerTodosAsync();

    Task<DetalleReparacion?> ObtenerPorIdAsync(int id);

    Task<List<DetalleReparacion>> ObtenerPorReparacionAsync(int idReparacion);

    Task AgregarAsync(DetalleReparacion detalle);

    Task ActualizarAsync(DetalleReparacion detalle, int idStockAnterior, int cantidadAnterior);

    Task EliminarAsync(int id);
}

public class RepositorioDetalleReparacion : IDetalleReparacionRepositorio
{
    private readonly AppDbContext _context;

    public RepositorioDetalleReparacion(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<DetalleReparacion>> ObtenerTodosAsync()
    {
        return await _context.DetalleReparacion.ToListAsync();
    }

    public async Task<DetalleReparacion?> ObtenerPorIdAsync(int id)
    {
        return await _context.DetalleReparacion.FindAsync(id);
    }

    public async Task<List<DetalleReparacion>> ObtenerPorReparacionAsync(int idReparacion)
    {
        return await _context.DetalleReparacion
            .Where(d => d.idReparacion == idReparacion)
            .ToListAsync();
    }

    //descuenta del stock la cantidad usada y guarda todo junto
    public async Task AgregarAsync(DetalleReparacion detalle)
    {
        await AjustarStockAsync(detalle.idStock, -detalle.Cantidad_Usada);

        _context.DetalleReparacion.Add(detalle);
        await _context.SaveChangesAsync();
    }

    //devuelve al stock la cantidad anterior y descuenta la nueva
    public async Task ActualizarAsync(DetalleReparacion detalle, int idStockAnterior, int cantidadAnterior)
    {
        await AjustarStockAsync(idStockAnterior, cantidadAnterior);
        await AjustarStockAsync(detalle.idStock, -detalle.Cantidad_Usada);

        _context.DetalleReparacion.Update(detalle);
        await _context.SaveChangesAsync();
    }

    //devuelve al stock la cantidad usada
    public async Task EliminarAsync(int id)
    {
        var detalle = await _context.DetalleReparacion.FindAsync(id);

        if (detalle is not null)
        {
            await AjustarStockAsync(detalle.idStock, detalle.Cantidad_Usada);

            _context.DetalleReparacion.Remove(detalle);
            await _context.SaveChangesAsync();
        }
    }

    private async Task AjustarStockAsync(int idStock, int cantidad)
    {
        var stock = await _context.Stock.FindAsync(idStock);
        if (stock is not null)
            stock.Cantidad_Stock += cantidad;
    }
}
