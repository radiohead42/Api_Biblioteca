using Api_Biblioteca.Datos;
using Api_Biblioteca.DTOs;
using Api_Biblioteca.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Biblioteca.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LibroDTO>> Get()
        {
            var libros = await context.Libros.ToListAsync();
            var librosDTO = mapper.Map<IEnumerable<LibroDTO>>(libros);
            return librosDTO;
        }

        [HttpGet("{id:int}", Name = "ObtenerLibro")]
        public async Task<ActionResult<LibroConAutorDTO>> Get(int id)
        {
            var libros = await context.Libros
                .Include(x => x.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (libros is null)
            {
                return NotFound();
            }

            var librosDTO = mapper.Map<LibroConAutorDTO>(libros);

            return Ok(librosDTO);

        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            var libro = mapper.Map<Libro>(libroCreacionDTO);
            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!existeAutor)
            {
                ModelState.AddModelError(nameof(libro.AutorId), "No existe el autor");
                return ValidationProblem();
            }
            context.Add(libro);
            await context.SaveChangesAsync();
            var libroDTO = mapper.Map<LibroDTO>(libro);
            return CreatedAtRoute("ObtenerLibro", new { id = libro.Id}, libroDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, LibroCreacionDTO libroCreacionDTO)
        {

            var libro = mapper.Map<Libro>(libroCreacionDTO);
            libro.Id = id;
            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!existeAutor)
            {
                return BadRequest("No existe el autor");
            }

            context.Update(libro);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var resultado = await context.Libros.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (resultado == 0)
            {
                return NotFound();
            }

            return NoContent();

        }

    }
}
