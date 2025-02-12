using Api_Biblioteca.Datos;
using Api_Biblioteca.DTOs;
using Api_Biblioteca.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Biblioteca.Controllers
{
    [ApiController]
    [Route("api/autores")] 
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AutorDTO>> Get()
        {
            var autores = await context.Autores.ToListAsync();
            //Transforma el tipo de dato al DTO
            //mapea usando la libreria mapper regresando un tipo de dato ienumerable del tipo autprdto
            //del resultado de la consultas al modelo de autores
            var autoresDTO = mapper.Map<IEnumerable<AutorDTO>>(autores);
            return autoresDTO;
        }

        //FromRoute indica que le parametro proviene de la misma ruta//?incluirLibros=false|true
        [HttpGet("{id:int}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutorConLibrosDTO>> Get([FromRoute] int id)
        {
            var autor = await context.Autores
                .Include(x => x.Libros)
                .ThenInclude(x => x.Libro)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(autor is null) 
            {
                return NotFound();
            }

            var autorDTO = mapper.Map<AutorConLibrosDTO>(autor);

            return autorDTO;

        }
        //FromBody los parametros se envian por el cuerpo de la peticion
        [HttpPost]
        public async Task<ActionResult>Post(AutorCreacionDTO autorCreacionDTO)
        {
            var autor = mapper.Map<Autor>(autorCreacionDTO);
            context.Add(autor);
            await context.SaveChangesAsync();
            var autorDTO = mapper.Map<AutorDTO>(autor);
            return CreatedAtRoute("ObtenerAutor", new {id = autor.Id}, autorDTO);
            //se llama al endpoint y se le pasan los parametros necesarios para su invocacion
            //al final se pasa al cliente el autor recien creado
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, AutorCreacionDTO autorCreacionDTO)
        {
            var autor = mapper.Map<Autor>(autorCreacionDTO);
            autor.Id = id;
            context.Update(autor);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<AutorPatchDTO> patchDoc)
        {
            if(patchDoc is null)
            {
                return BadRequest();
            }

            var autorDB = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autorDB is null)
            {
                return NotFound();
            }

            var autorPatchDTO = mapper.Map<AutorPatchDTO>(autorDB);

            patchDoc.ApplyTo(autorPatchDTO, ModelState);

            var esValido = TryValidateModel(autorPatchDTO);

            if (!esValido)
            {
                return ValidationProblem();
            }

            mapper.Map(autorPatchDTO, autorDB);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrado = await context.Autores.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrado == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
