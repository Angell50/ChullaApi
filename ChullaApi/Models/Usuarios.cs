﻿using System.ComponentModel.DataAnnotations;
using SQLite;

namespace ChullaApi.Models
{
    public class Usuario
    {
        [Key]
        [PrimaryKey, AutoIncrement]
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