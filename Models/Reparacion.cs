using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabajo_Final_Mobile_Api_2026.Models;

[Table("reparacion")]
public class Reparacion()
{
    [Key]
    public int id_Reparacion  { get; set; }

    public int idUsuario { get; set; }
    public string idVehiculo { get; set; } = "";

    public string Nombre_Cliente { get; set; } = "";

    public DateOnly Fecha_Ingreso {get; set;}


    public string Descripcion_Trabajo_Realizado { get; set; } = "";

    public string Motivo_Ingreso { get; set; } = "";

    public decimal Costo_Mano_De_Obra { get; set; }

    public DateOnly Fecha_Creacion { get; set; }
}
