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
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepositorio _repositorio;
    private readonly IConfiguration _configuration;

    public UsuarioController(IUsuarioRepositorio repositorio, IConfiguration configuration)
    {
        _repositorio = repositorio;
        _configuration = configuration;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var usuario = await _repositorio.ObtenerPorEmailAsync(request.Email);
        if (usuario is null)
            return Unauthorized();

        var salt = _configuration["Salt"] ?? "";
        var claveHash = ClaveHelper.Hashear(request.Clave, salt);
        if (usuario.Clave != claveHash)
            return Unauthorized();

        var rolNombre = usuario.Rol switch
        {
            1 => "Administrador",
            2 => "Empleado",
            _ => ""
        };

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.id_Usuario.ToString()),
            new(ClaimTypes.Email, usuario.Email),
            new(ClaimTypes.Role, rolNombre)
        };

        var token = TokenHelper.GenerarToken(_configuration, claims);

        return Ok(new LoginResponseDto
        {
            Token = token,
            Usuario = UsuarioDto.DesdeUsuario(usuario)
        });
    }

    [HttpGet]
    [Authorize(Policy = "Administrador")]
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

    [HttpPost]
    [Authorize(Policy = "Administrador")]
    public async Task<IActionResult> PostAgregarUsuario(Usuario request)
    {
        var existente = await _repositorio.ObtenerPorEmailAsync(request.Email);
        if (existente is not null)
            return Conflict("El email ya esta registrado.");

        var salt = _configuration["Salt"] ?? "";

        var usuario = new Usuario
        {
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Especializacion = request.Especializacion,
            Email = request.Email,
            Clave = ClaveHelper.Hashear(request.Clave, salt),
            Rol = request.Rol
        };

        await _repositorio.AgregarAsync(usuario);

        return NoContent();
    }







    
}
