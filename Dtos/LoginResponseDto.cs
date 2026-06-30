namespace Trabajo_Final_Mobile_Api_2026.Dtos;

public class LoginResponseDto
{
    public string Token { get; set; } = "";
    public UsuarioDto Usuario { get; set; } = null!;
}
