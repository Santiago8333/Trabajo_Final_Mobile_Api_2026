using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabajo_Final_Mobile_Api_2026.Models;

[Table("detalle_reparacion")]
public class DetalleReparacion
{
    [Key]
    public int Id_Detalle { get; set; }

    public int idStock { get; set; }
    public int idReparacion { get; set; }

    public int Cantidad_Usada { get; set; }

    public decimal Precio_Unitario_Momento { get; set; }

    public decimal Subtotal { get; set; }

    public DateOnly Fecha_Consumo { get; set; }
}
