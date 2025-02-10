﻿using System.ComponentModel.DataAnnotations;

namespace Api_Biblioteca.DTOs
{
    public class ComentarioDTO
    {
        public Guid Id { get; set; }
        public required string Cuerpo { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
