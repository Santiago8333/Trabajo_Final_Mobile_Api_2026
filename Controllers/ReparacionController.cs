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
public class ReparacionController : ControllerBase
{
    private readonly IReparacionRepositorio _repositorio;
    private readonly IDetalleReparacionRepositorio _detalleRepositorio;
    private readonly IConfiguration _configuration;

    public ReparacionController(IReparacionRepositorio repositorio,IDetalleReparacionRepositorio detalleRepositorio,IConfiguration configuration)
    {
        _repositorio = repositorio;
        _detalleRepositorio = detalleRepositorio;
        _configuration = configuration;
    }

     //traer todo
    [HttpGet]
    public async Task<ActionResult<List<Reparacion>>> Get()
    {
        var reparacions = await _repositorio.ObtenerTodosAsync();
        return Ok(reparacions);

    }

    //traer por id 
    [HttpGet("{id}")]
    public async Task<ActionResult<Reparacion>> GetPorId(int id)
    {
        var reparacion = await _repositorio.ObtenerPorIdAsync(id);
        if (reparacion is null)
            return NotFound();

        return Ok(reparacion);
    }

    //agregar
    [HttpPost]
    public async Task<IActionResult> Post(Reparacion request)
    {
        var reparacion = new Reparacion
        {
            idUsuario = request.idUsuario,
            idVehiculo = request.idVehiculo,
            Nombre_Cliente = request.Nombre_Cliente,
            Fecha_Ingreso = request.Fecha_Ingreso,
            Descripcion_Trabajo_Realizado = request.Descripcion_Trabajo_Realizado,
            Motivo_Ingreso = request.Motivo_Ingreso,
            Costo_Mano_De_Obra = request.Costo_Mano_De_Obra,
            Fecha_Creacion = request.Fecha_Creacion
        };

        await _repositorio.AgregarAsync(reparacion);

        return NoContent();
    }

    //actualizar
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Reparacion request)
    {
        var reparacion = await _repositorio.ObtenerPorIdAsync(id);
        if (reparacion is null)
            return NotFound();

        reparacion.idUsuario = request.idUsuario;
        reparacion.idVehiculo = request.idVehiculo;
        reparacion.Nombre_Cliente = request.Nombre_Cliente;
        reparacion.Fecha_Ingreso = request.Fecha_Ingreso;
        reparacion.Descripcion_Trabajo_Realizado = request.Descripcion_Trabajo_Realizado;
        reparacion.Motivo_Ingreso = request.Motivo_Ingreso;
        reparacion.Costo_Mano_De_Obra = request.Costo_Mano_De_Obra;

        await _repositorio.ActualizarAsync(reparacion);

        return NoContent();
    }

    //eliminar
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var reparacion = await _repositorio.ObtenerPorIdAsync(id);
        if (reparacion is null)
            return NotFound();

        await _repositorio.EliminarAsync(id);

        return NoContent();
    }

    // ----- Detalle de reparacion -----

    //traer todos los detalles
    [HttpGet("detalle")]
    public async Task<ActionResult<List<DetalleReparacion>>> GetDetalles()
    {
        var detalleReparacions = await _detalleRepositorio.ObtenerTodosAsync();
        return Ok(detalleReparacions);
    }

    //traer detalle por id
    [HttpGet("detalle/{id}")]
    public async Task<ActionResult<DetalleReparacion>> GetDetallePorId(int id)
    {
        var detalleReparacion = await _detalleRepositorio.ObtenerPorIdAsync(id);
        if (detalleReparacion is null)
            return NotFound();

        return Ok(detalleReparacion);
    }

    //traer los detalles de una reparacion
    [HttpGet("{idReparacion}/detalle")]
    public async Task<ActionResult<List<DetalleReparacion>>> GetDetallesPorReparacion(int idReparacion)
    {
        var detalleReparacions = await _detalleRepositorio.ObtenerPorReparacionAsync(idReparacion);
        return Ok(detalleReparacions);
    }

    //agregar detalle
    [HttpPost("detalle")]
    public async Task<IActionResult> PostDetalle(DetalleReparacion request)
    {
        var detalleReparacion = new DetalleReparacion
        {
            idStock = request.idStock,
            idReparacion = request.idReparacion,
            Cantidad_Usada = request.Cantidad_Usada,
            Precio_Unitario_Momento = request.Precio_Unitario_Momento,
            Subtotal = request.Cantidad_Usada * request.Precio_Unitario_Momento,
            Fecha_Consumo = request.Fecha_Consumo
        };

        await _detalleRepositorio.AgregarAsync(detalleReparacion);

        return NoContent();
    }

    //actualizar detalle
    [HttpPut("detalle/{id}")]
    public async Task<IActionResult> PutDetalle(int id, DetalleReparacion request)
    {
        var detalleReparacion = await _detalleRepositorio.ObtenerPorIdAsync(id);
        if (detalleReparacion is null)
            return NotFound();

        detalleReparacion.idStock = request.idStock;
        detalleReparacion.idReparacion = request.idReparacion;
        detalleReparacion.Cantidad_Usada = request.Cantidad_Usada;
        detalleReparacion.Precio_Unitario_Momento = request.Precio_Unitario_Momento;
        detalleReparacion.Subtotal = request.Cantidad_Usada * request.Precio_Unitario_Momento;

        await _detalleRepositorio.ActualizarAsync(detalleReparacion);

        return NoContent();
    }

    //eliminar detalle
    [HttpDelete("detalle/{id}")]
    public async Task<IActionResult> DeleteDetalle(int id)
    {
        var detalleReparacion = await _detalleRepositorio.ObtenerPorIdAsync(id);
        if (detalleReparacion is null)
            return NotFound();

        await _detalleRepositorio.EliminarAsync(id);

        return NoContent();
    }

}