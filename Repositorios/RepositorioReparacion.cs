using Microsoft.EntityFrameworkCore;
using Trabajo_Final_Mobile_Api_2026.Models;

namespace Trabajo_Final_Mobile_Api_2026.Repositorios;

public interface IReparacionRepositorio
{
    Task<List<Reparacion>> ObtenerTodosAsync();

    Task<Reparacion?> ObtenerPorIdAsync(int id);

    Task AgregarAsync(Reparacion reparacion);

    Task ActualizarAsync(Reparacion reparacion);

    Task EliminarAsync(int id);
}

public class RepositorioReparacion : IReparacionRepositorio
{
    private readonly AppDbContext _context;

    public RepositorioReparacion(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Reparacion>> ObtenerTodosAsync()
    {
        return await _context.Reparacion.ToListAsync();
    }

    public async Task<Reparacion?> ObtenerPorIdAsync(int id)
    {
        return await _context.Reparacion.FindAsync(id);
    }

    public async Task AgregarAsync(Reparacion reparacion)
    {
        _context.Reparacion.Add(reparacion);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Reparacion reparacion)
    {
        _context.Reparacion.Update(reparacion);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var reparacion = await _context.Reparacion.FindAsync(id);

        if (reparacion is not null)
        {
            _context.Reparacion.Remove(reparacion);
            await _context.SaveChangesAsync();
        }
    }
}
