using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabajo_Final_Mobile_Api_2026.Models;

[Table("factura")]
public class Factura
{
    [Key]
    public int id_Factura { get; set; }

    public int idReparacion { get; set; }

    public string Total_Factura { get; set; } = "";

    public bool Estado { get; set; }

}
