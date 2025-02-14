using System.ComponentModel.DataAnnotations;

namespace Api_Biblioteca
{
    public class PersonaOpciones
    {
        public const string Seccion = "seccion_1";
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public int Edad { get; set; }
    }
}
