using Api_Biblioteca.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Api_Biblioteca.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
    }
}
