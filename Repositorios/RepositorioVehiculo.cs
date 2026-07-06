using Microsoft.EntityFrameworkCore;
using Trabajo_Final_Mobile_Api_2026.Models;

namespace Trabajo_Final_Mobile_Api_2026.Repositorios;

public interface IVehiculoRepositorio
{
    Task<List<Vehiculo>> ObtenerTodosAsync();

    Task<Vehiculo?> ObtenerPorMatriculaAsync(string matricula);

    Task AgregarAsync(Vehiculo vehiculo);

    Task ActualizarAsync(Vehiculo vehiculo);

    Task EliminarAsync(string matricula);
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

    public async Task<Vehiculo?> ObtenerPorMatriculaAsync(string matricula)
    {
        return await _context.Vehiculo.FindAsync(matricula);
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

    public async Task EliminarAsync(string matricula)
    {
        var vehiculo = await _context.Vehiculo.FindAsync(matricula);

        if (vehiculo is not null)
        {
            _context.Vehiculo.Remove(vehiculo);
            await _context.SaveChangesAsync();
        }
    }
}
