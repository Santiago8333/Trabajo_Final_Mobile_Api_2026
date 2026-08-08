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
public class FacturaController : ControllerBase
{
    private readonly IFacturaRepositorio _repositorio;
    private readonly IConfiguration _configuration;

    public FacturaController(IFacturaRepositorio repositorio,IConfiguration configuration)
    {
        _repositorio = repositorio;
        _configuration = configuration;
    }


    [HttpGet]
    public async Task<ActionResult<List<Factura>>> Get()
    {
        var factura = await _repositorio.ObtenerTodosAsync();
        return Ok(factura);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Factura>> GetPorId(int id)
    {
        var factura = await _repositorio.ObtenerPorIdAsync(id);
        if (factura is null)
            return NotFound();

        return Ok(factura);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Factura request)
    {
        var factura = new Factura
        {
            idReparacion = request.idReparacion,
            Total_Factura = request.Total_Factura,
            Estado = request.Estado
        };

        await _repositorio.AgregarAsync(factura);

        return NoContent();
    }

    //actualizar
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Factura request)
    {
        var factura = await _repositorio.ObtenerPorIdAsync(id);
        if (factura is null)
            return NotFound();

        factura.idReparacion = request.idReparacion;
        factura.Total_Factura = request.Total_Factura;

        await _repositorio.ActualizarAsync(factura);

        return NoContent();
    }

    //cambiar estado
    [HttpPut("{id}/estado")]
    public async Task<IActionResult> PutEstado(int id, CambiarEstadoDto request)
    {
        var factura = await _repositorio.ObtenerPorIdAsync(id);
        if (factura is null)
            return NotFound();

        factura.Estado = request.Estado;

        await _repositorio.ActualizarAsync(factura);

        return NoContent();
    }

    //eliminar
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var factura = await _repositorio.ObtenerPorIdAsync(id);
        if (factura is null)
            return NotFound();

        await _repositorio.EliminarAsync(id);

        return NoContent();
    }

}