using Microsoft.EntityFrameworkCore;
using Trabajo_Final_Mobile_Api_2026.Models;

namespace Trabajo_Final_Mobile_Api_2026.Repositorios;

public interface IFacturaRepositorio
{
    Task<List<Factura>> ObtenerTodosAsync();

    Task<Factura?> ObtenerPorIdAsync(int id);

    Task AgregarAsync(Factura factura);

    Task ActualizarAsync(Factura factura);

    Task EliminarAsync(int id);

    Task<List<Factura>> ObtenerPorReparacionAsync(int idReparacion);

    Task<List<Factura>> ObtenerPorEstadoAsync(bool estado);
}

public class RepositorioFactura : IFacturaRepositorio
{
    private readonly AppDbContext _context;

    public RepositorioFactura(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Factura>> ObtenerTodosAsync()
    {
        return await _context.Factura.ToListAsync();
    }

    public async Task<Factura?> ObtenerPorIdAsync(int id)
    {
        return await _context.Factura.FindAsync(id);
    }

    public async Task AgregarAsync(Factura factura)
    {
        _context.Factura.Add(factura);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Factura factura)
    {
        _context.Factura.Update(factura);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var factura = await _context.Factura.FindAsync(id);

        if (factura is not null)
        {
            _context.Factura.Remove(factura);
            await _context.SaveChangesAsync();
        }
    }


    public async Task<List<Factura>> ObtenerPorReparacionAsync(int idReparacion)
    {
        return await _context.Factura
            .Where(f => f.idReparacion == idReparacion)
            .ToListAsync();
    }

    public async Task<List<Factura>> ObtenerPorEstadoAsync(bool estado)
    {
        return await _context.Factura
            .Where(f => f.Estado == estado)
            .ToListAsync();
    }
}
