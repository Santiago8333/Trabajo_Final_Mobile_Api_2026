using Microsoft.EntityFrameworkCore;
using Trabajo_Final_Mobile_Api_2026.Models;

namespace Trabajo_Final_Mobile_Api_2026.Repositorios;

public interface IVehiculoRepositorio
{
    Task<List<Vehiculo>> ObtenerTodosAsync();

    Task<Vehiculo?> ObtenerPorIdAsync(int id);

    Task<Vehiculo?> ObtenerPorMatriculaAsync(string matricula);

    Task AgregarAsync(Vehiculo vehiculo);

    Task ActualizarAsync(Vehiculo vehiculo);

    Task EliminarAsync(int id);

    Task<List<Vehiculo>> BuscarAsync(string texto);

    Task<List<Vehiculo>> ObtenerPaginadoAsync(int pagina, int tamanio);

    Task<int> ContarAsync();
}

public class RepositorioVehiculo : IVehiculoRepositorio
{
    private readonly AppDbContext _context;

    public RepositorioVehiculo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Vehiculo>> ObtenerTodosAsync()
    {
        return await _context.Vehiculo.ToListAsync();
    }

    public async Task<Vehiculo?> ObtenerPorIdAsync(int id)
    {
        return await _context.Vehiculo.FindAsync(id);
    }

    public async Task<Vehiculo?> ObtenerPorMatriculaAsync(string matricula)
    {
        return await _context.Vehiculo
            .FirstOrDefaultAsync(v => v.Matricula == matricula);
    }

    public async Task AgregarAsync(Vehiculo vehiculo)
    {
        _context.Vehiculo.Add(vehiculo);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Vehiculo vehiculo)
    {
        _context.Vehiculo.Update(vehiculo);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var vehiculo = await _context.Vehiculo.FindAsync(id);

        if (vehiculo is not null)
        {
            _context.Vehiculo.Remove(vehiculo);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Vehiculo>> BuscarAsync(string texto)
    {
        return await _context.Vehiculo
            .Where(u => u.Matricula.Contains(texto)
                     || u.Modelo.Contains(texto))
            .ToListAsync();
    }

    public async Task<List<Vehiculo>> ObtenerPaginadoAsync(int pagina, int tamanio)
    {
        return await _context.Vehiculo
            .OrderBy(v => v.id_Vehiculo)
            .Skip((pagina - 1) * tamanio)
            .Take(tamanio)
            .ToListAsync();
    }

    public async Task<int> ContarAsync()
    {
        return await _context.Vehiculo.CountAsync();
    }

}
