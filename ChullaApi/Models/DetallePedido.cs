using System.ComponentModel.DataAnnotations;
using SQLite;

namespace ChullaApi.Models
{
    public class DetallePedido
    {
        [Key]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public int PedidoId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal Precio { get; set; }
    }
}