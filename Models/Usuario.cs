using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabajo_Final_Mobile_Api_2026.Models;

[Table("usuario")]
public class Usuario()
{
    [Key]
    public int id_Usuario  { get; set; }

    public string Nombre { get; set; } = "";

    public string Apellido { get; set; } = "";

    public string Especializacion {get;set;} = "";

    public string Email {get;set;} = "";

    public string Clave {get;set;} = "";

    public int Rol  { get; set; }

    public string Avatar { get; set; } = "";

    public DateOnly Fecha_Creacion {get;set;}


}
