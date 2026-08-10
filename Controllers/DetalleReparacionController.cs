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
public class DetalleReparacionController : ControllerBase
{
    private readonly IDetalleReparacionRepositorio _repositorio;
    private readonly IConfiguration _configuration;

     public DetalleReparacionController(IDetalleReparacionRepositorio repositorio,IConfiguration configuration)
    {
        _repositorio = repositorio;
        _configuration = configuration;
    }

    //traer todo
    [HttpGet]
    public async Task<ActionResult<List<DetalleReparacion>>> Get()
    {
        var detalleReparacions = await _repositorio.ObtenerTodosAsync();
        return Ok(detalleReparacions);

    }

    //traer por id 
    [HttpGet("{id}")]
    public async Task<ActionResult<DetalleReparacion>> GetPorId(int id)
    {
        var detalleReparacion = await _repositorio.ObtenerPorIdAsync(id);
        if (detalleReparacion is null)
            return NotFound();

        return Ok(detalleReparacion);
    }

    //agregar
    [HttpPost]
    public async Task<IActionResult> Post(DetalleReparacion request)
    {
        var detalleReparacion = new DetalleReparacion
        {
            idStock = request.idStock,
            idReparacion = request.idReparacion,
            Cantidad_Usada = request.Cantidad_Usada,

        };

        await _repositorio.AgregarAsync(detalleReparacion);

        return NoContent();
    }









}