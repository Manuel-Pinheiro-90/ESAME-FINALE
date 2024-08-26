﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Api_Finale.Models
{
    public class Utente
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nome { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public required string PasswordHash { get; set; }

        [Required]
        public DateTime DataRegistrazione { get; set; }


        [Column(TypeName = "nvarchar(max)")]
        public string? Foto { get; set; } 

        
        public List<Ruolo> Ruoli { get; set; } = [];

        
        public List<Evento> EventiCreati { get; set; } = [];
        public List<Registrazione> Registrazioni { get; set; } = [];
        public List<Personaggio> Personaggi { get; set; } = [];


    }
}
