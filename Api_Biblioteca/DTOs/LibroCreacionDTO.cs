using Api_Biblioteca.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Api_Biblioteca.DTOs
{
    public class LibroCreacionDTO
    {
        [Required]
        [StringLength(250, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        public required string Titulo { get; set; }
        public int AutorId { get; set; }
        public Autor? Autor { get; set; }
    }
}
