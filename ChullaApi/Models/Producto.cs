using System.ComponentModel.DataAnnotations;
using SQLite;

namespace ChullaApi.Models
{
    public class Producto
    {
        [Key]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string Categoria { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(500)]
        public string Descripcion { get; set; }
    }
}