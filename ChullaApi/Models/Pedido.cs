using System.ComponentModel.DataAnnotations;
using SQLite;

namespace ChullaApi.Models
{
    public class Pedido
    {
        [Key]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public string FechaPedido { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public decimal Total { get; set; }
    }
}