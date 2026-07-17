using Microsoft.EntityFrameworkCore;
using Trabajo_Final_Mobile_Api_2026.Models;

namespace Trabajo_Final_Mobile_Api_2026.Repositorios;

public interface IUsuarioRepositorio
{
    Task<List<Usuario>> ObtenerTodosAsync();
    Task<List<Usuario>> BuscarAsync(string texto);
    Task<Usuario?> ObtenerPorIdAsync(int id);
    Task<Usuario?> ObtenerPorEmailAsync(string email);
    Task AgregarAsync(Usuario usuario);
    Task ActualizarAsync(Usuario usuario);
    Task EliminarAsync(int id);
}

public class RepositorioUsuario : IUsuarioRepositorio
{
    private readonly AppDbContext _context;

    public RepositorioUsuario(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> ObtenerTodosAsync()
    {
        return await _context.Usuario.ToListAsync();
    }

    public async Task<List<Usuario>> BuscarAsync(string texto)
    {
        return await _context.Usuario
            .Where(u => u.Email.Contains(texto)
                     || u.Nombre.Contains(texto)
                     || u.Apellido.Contains(texto))
            .ToListAsync();
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        return await _context.Usuario.FindAsync(id);
    }

    public async Task<Usuario?> ObtenerPorEmailAsync(string email)
    {
        return await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AgregarAsync(Usuario usuario)
    {
        _context.Usuario.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Usuario usuario)
    {
        _context.Usuario.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario is not null)
        {
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
