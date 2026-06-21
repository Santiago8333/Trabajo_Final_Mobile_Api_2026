using Microsoft.AspNetCore.Mvc;
using Trabajo_Final_Mobile_Api_2026.Models;
using Trabajo_Final_Mobile_Api_2026.Repositorios;

namespace Trabajo_Final_Mobile_Api_2026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepositorio _repositorio;

    public UsuarioController(IUsuarioRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    [HttpGet]
    public async Task<ActionResult<List<UsuarioDto>>> Get()
    {
        var usuarios = await _repositorio.ObtenerTodosAsync();
        return Ok(usuarios.Select(UsuarioDto.DesdeUsuario));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDto>> GetPorId(int id)
    {
        var usuario = await _repositorio.ObtenerPorIdAsync(id);
        if (usuario is null)
            return NotFound();

        return Ok(UsuarioDto.DesdeUsuario(usuario));
    }
}
