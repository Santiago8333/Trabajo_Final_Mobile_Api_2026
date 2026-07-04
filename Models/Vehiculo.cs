using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabajo_Final_Mobile_Api_2026.Models;

[Table("vehiculo")]
public class Vehiculo
{
    [Key]
    public string Matricula { get; set; } = "";

    public string Modelo { get; set; } = "";

    public DateOnly Fecha_Creacion { get; set; }
}
