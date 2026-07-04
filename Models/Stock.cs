using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabajo_Final_Mobile_Api_2026.Models;

[Table("stock")]
public class Stock
{
    [Key]
    public int id_Stock { get; set; }

    public string Nombre_Pieza { get; set; } = "";

    public int Cantidad_Stock { get; set; }

    public decimal Precio_Unitario { get; set; }

    public DateOnly Fecha_Creacion { get; set; }
}
