using Microsoft.EntityFrameworkCore;
using Trabajo_Final_Mobile_Api_2026.Models;

namespace Trabajo_Final_Mobile_Api_2026.Repositorios;

public interface IStockRepositorio
{
    Task<List<Stock>> ObtenerTodosAsync();

    Task<Stock?> ObtenerPorIdAsync(int id);

    Task AgregarAsync(Stock stock);

    Task ActualizarAsync(Stock stock);

    Task EliminarAsync(int id);


}

public class RepositorioStock : IStockRepositorio
{
    private readonly AppDbContext _context;

    public RepositorioStock(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> ObtenerTodosAsync()
    {
        return await _context.Stock.ToListAsync();
    }
    
    public async Task<Stock?> ObtenerPorIdAsync(int id)
    {
        return await _context.Stock.FindAsync(id);
    }

    public async Task AgregarAsync(Stock stock)
    {
        _context.Stock.Add(stock);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Stock stock)
    {
        _context.Stock.Update(stock);
        await _context.SaveChangesAsync();
    }

     public async Task EliminarAsync(int id)
    {
        var stock = await _context.Stock.FindAsync(id);

         if (stock is not null)
        {
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
        }

    }


}