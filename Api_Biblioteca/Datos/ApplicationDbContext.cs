using Api_Biblioteca.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Api_Biblioteca.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }

    }
}
