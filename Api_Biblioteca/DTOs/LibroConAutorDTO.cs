﻿namespace Api_Biblioteca.DTOs
{
    public class LibroConAutorDTO: LibroDTO
    {
        public int AutorId { get; set; }
        public required string AutorNombre { get; set; }
    }
}
