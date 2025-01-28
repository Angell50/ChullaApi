using System.ComponentModel.DataAnnotations;

namespace ChullaApi.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Contraseña { get; set; }

        public bool EsAdmin { get; set; } = false;
    }
}