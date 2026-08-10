using Microsoft.EntityFrameworkCore;
using Trabajo_Final_Mobile_Api_2026.Models;

namespace Trabajo_Final_Mobile_Api_2026.Repositorios;

public interface IDetalleReparacionRepositorio
{
    Task<List<DetalleReparacion>> ObtenerTodosAsync();

    Task<DetalleReparacion?> ObtenerPorIdAsync(int id);

    Task<List<DetalleReparacion>> ObtenerPorReparacionAsync(int idReparacion);

    Task AgregarAsync(DetalleReparacion detalle);

    Task ActualizarAsync(DetalleReparacion detalle);

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

    public async Task AgregarAsync(DetalleReparacion detalle)
    {
        _context.DetalleReparacion.Add(detalle);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(DetalleReparacion detalle)
    {
        _context.DetalleReparacion.Update(detalle);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var detalle = await _context.DetalleReparacion.FindAsync(id);

        if (detalle is not null)
        {
            _context.DetalleReparacion.Remove(detalle);
            await _context.SaveChangesAsync();
        }
    }
}
