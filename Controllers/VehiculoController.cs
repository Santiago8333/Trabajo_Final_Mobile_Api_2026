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
public class VehiculoController : ControllerBase
{
    private readonly IVehiculoRepositorio _repositorio;
    private readonly IConfiguration _configuration;

    public VehiculoController(IVehiculoRepositorio repositorio,IConfiguration configuration)
    {
        _repositorio = repositorio;
        _configuration = configuration;
    }


    [HttpGet]
    public async Task<ActionResult<List<Vehiculo>>> Get()
    {
        var vehiculos = await _repositorio.ObtenerTodosAsync();
        return Ok(vehiculos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vehiculo>> GetPorId(int id)
    {
        var vehiculo = await _repositorio.ObtenerPorIdAsync(id);
        if (vehiculo is null)
            return NotFound();

        return Ok(vehiculo);
    }

    [HttpGet("matricula/{matricula}")]
    public async Task<ActionResult<Vehiculo>> GetPorMatricula(string matricula)
    {
        var vehiculo = await _repositorio.ObtenerPorMatriculaAsync(matricula);
        if (vehiculo is null)
            return NotFound();

        return Ok(vehiculo);
    }

    //agregar
    [HttpPost]
    public async Task<IActionResult> Post(Vehiculo request)
    {
        var vehiculo = new Vehiculo
        {
            Matricula = request.Matricula,
            Modelo = request.Modelo,
            Fecha_Creacion = request.Fecha_Creacion
        };

        await _repositorio.AgregarAsync(vehiculo);

        return NoContent();
    }

    //actualizar
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Vehiculo request)
    {
        var vehiculo = await _repositorio.ObtenerPorIdAsync(id);
        if (vehiculo is null)
            return NotFound();

        vehiculo.Matricula = request.Matricula;
        vehiculo.Modelo = request.Modelo;

        await _repositorio.ActualizarAsync(vehiculo);

        return NoContent();
    }

    //eliminar
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var vehiculo = await _repositorio.ObtenerPorIdAsync(id);
        if (vehiculo is null)
            return NotFound();

        await _repositorio.EliminarAsync(id);

        return NoContent();
    }

    //buscar
    [HttpGet("buscar")]
    public async Task<ActionResult<List<Vehiculo>>> Buscar([FromQuery] string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return Ok(new List<Vehiculo>());

        var vehiculos = await _repositorio.BuscarAsync(texto);
        return Ok(vehiculos);
    }

    //paginado
    [HttpGet("paginado")]
    public async Task<IActionResult> GetPaginado([FromQuery] int pagina = 1, [FromQuery] int tamanio = 10)
    {
        if (pagina < 1)
            pagina = 1;

        if (tamanio < 1 || tamanio > 100)
            tamanio = 10;

        var vehiculos = await _repositorio.ObtenerPaginadoAsync(pagina, tamanio);
        var total = await _repositorio.ContarAsync();

        return Ok(new { total, pagina, tamanio, datos = vehiculos });
    }

}