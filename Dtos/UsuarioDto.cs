using Trabajo_Final_Mobile_Api_2026.Models;

namespace Trabajo_Final_Mobile_Api_2026.Dtos;

public class UsuarioDto
{
    public int id_Usuario { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Especializacion { get; set; } = "";
    public string Email { get; set; } = "";
    public int Rol { get; set; }
    public string Avatar { get; set; } = "";
    public DateOnly Fecha_Creacion { get; set; }

    public static UsuarioDto DesdeUsuario(Usuario usuario) => new()
    {
        id_Usuario = usuario.id_Usuario,
        Nombre = usuario.Nombre,
        Apellido = usuario.Apellido,
        Especializacion = usuario.Especializacion,
        Email = usuario.Email,
        Rol = usuario.Rol,
        Avatar = usuario.Avatar,
        Fecha_Creacion = usuario.Fecha_Creacion
    };
}
