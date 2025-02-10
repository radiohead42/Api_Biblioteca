using System.ComponentModel.DataAnnotations;

namespace Api_Biblioteca.DTOs
{
    public class ComentarioCreacionDTO
    {
        [Required]
        public required string Cuerpo { get; set; }
    }
}
