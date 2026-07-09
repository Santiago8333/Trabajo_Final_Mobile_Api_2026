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
    private readonly IWebHostEnvironment _entorno;

    public UsuarioController(IUsuarioRepositorio repositorio, IConfiguration configuration, IWebHostEnvironment entorno)
    {
        _repositorio = repositorio;
        _configuration = configuration;
        _entorno = entorno;
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
            Rol = request.Rol,
            Avatar = "avatars/default-avatar.png"
        };

        await _repositorio.AgregarAsync(usuario);

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "Administrador")]
    public async Task<IActionResult> Put(int id, Usuario request)
    {
        var usuario = await _repositorio.ObtenerPorIdAsync(id);
        if (usuario is null)
            return NotFound();

        var existente = await _repositorio.ObtenerPorEmailAsync(request.Email);
        if (existente is not null && existente.id_Usuario != id)
            return Conflict("El email ya esta registrado.");

        usuario.Nombre = request.Nombre;
        usuario.Apellido = request.Apellido;
        usuario.Especializacion = request.Especializacion;
        usuario.Email = request.Email;
        usuario.Rol = request.Rol;

        await _repositorio.ActualizarAsync(usuario);

        return NoContent();
    }

    [HttpPut("{id}/clave")]
    [Authorize(Policy = "Administrador")]
    public async Task<IActionResult> CambiarClave(int id, CambiarClaveDto request)
    {
        var usuario = await _repositorio.ObtenerPorIdAsync(id);
        if (usuario is null)
            return NotFound();

        var salt = _configuration["Salt"] ?? "";
        usuario.Clave = ClaveHelper.Hashear(request.Clave, salt);

        await _repositorio.ActualizarAsync(usuario);

        return NoContent();
    }

    [HttpPost("{id}/avatar")]
    public async Task<IActionResult> SubirAvatar(int id, IFormFile archivo)
    {
        var usuario = await _repositorio.ObtenerPorIdAsync(id);
        if (usuario is null)
            return NotFound();

        if (archivo is null || archivo.Length == 0)
            return BadRequest("No se envio ningun archivo.");

        var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();
        var permitidas = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        if (!permitidas.Contains(extension))
            return BadRequest("Formato no permitido. Use jpg, jpeg, png o webp.");

        var carpeta = Path.Combine(_entorno.WebRootPath ?? Path.Combine(_entorno.ContentRootPath, "wwwroot"), "avatars");
        Directory.CreateDirectory(carpeta);

        // Elimina el avatar anterior
        EliminarArchivoAvatar(usuario.Avatar);

        var nombreArchivo = $"usuario_{id}{extension}";
        var rutaFisica = Path.Combine(carpeta, nombreArchivo);

        using (var stream = new FileStream(rutaFisica, FileMode.Create))
        {
            await archivo.CopyToAsync(stream);
        }

        usuario.Avatar = $"avatars/{nombreArchivo}";
        await _repositorio.ActualizarAsync(usuario);

        return Ok(new { avatar = usuario.Avatar });
    }

    private void EliminarArchivoAvatar(string avatar)
    {
       
        if (string.IsNullOrEmpty(avatar) || avatar == "avatars/default-avatar.png")
            return;

        var raiz = _entorno.WebRootPath ?? Path.Combine(_entorno.ContentRootPath, "wwwroot");
        var rutaFisica = Path.Combine(raiz, avatar.Replace('/', Path.DirectorySeparatorChar));

        if (System.IO.File.Exists(rutaFisica))
            System.IO.File.Delete(rutaFisica);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Administrador")]
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _repositorio.ObtenerPorIdAsync(id);
        if (usuario is null)
            return NotFound();

        EliminarArchivoAvatar(usuario.Avatar);

        await _repositorio.EliminarAsync(id);

        return NoContent();
    }




    
}
