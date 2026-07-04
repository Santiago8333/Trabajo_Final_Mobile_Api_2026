using Microsoft.EntityFrameworkCore;
using Trabajo_Final_Mobile_Api_2026.Models;

namespace Trabajo_Final_Mobile_Api_2026.Repositorios;

public interface IStockRepositorio
{
    

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
    

}