using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Trabajo_Final_Mobile_Api_2026.Dtos;
using Trabajo_Final_Mobile_Api_2026.Helpers;
using Trabajo_Final_Mobile_Api_2026.Models;
using Trabajo_Final_Mobile_Api_2026.Repositorios;

namespace Trabajo_Final_Mobile_Api_2026.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StockController : ControllerBase
{
    private readonly IStockRepositorio _repositorio;
    private readonly IConfiguration _configuration;

    public StockController(IStockRepositorio repositorio,IConfiguration configuration)
    {
        _repositorio = repositorio;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<ActionResult<List<Stock>>> Get()
    {
        var stock = await _repositorio.ObtenerTodosAsync();
        return Ok(stock);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Stock>> GetPorId(int id)
    {
        var stock = await _repositorio.ObtenerPorIdAsync(id);
        if (stock is null)
            return NotFound();

        return Ok(stock);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Stock request)
    {
        var stock = new Stock
        {
            Nombre_Pieza = request.Nombre_Pieza,
            Cantidad_Stock = request.Cantidad_Stock,
            Precio_Unitario = request.Precio_Unitario,
            Fecha_Creacion = request.Fecha_Creacion
        };

        await _repositorio.AgregarAsync(stock);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Stock request)
    {
        var stock = await _repositorio.ObtenerPorIdAsync(id);
        if (stock is null)
            return NotFound();

        stock.Nombre_Pieza = request.Nombre_Pieza;
        stock.Cantidad_Stock = request.Cantidad_Stock;
        stock.Precio_Unitario = request.Precio_Unitario;

        await _repositorio.ActualizarAsync(stock);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var stock = await _repositorio.ObtenerPorIdAsync(id);
        if (stock is null)
            return NotFound();

        await _repositorio.EliminarAsync(id);

        return NoContent();
    }



}