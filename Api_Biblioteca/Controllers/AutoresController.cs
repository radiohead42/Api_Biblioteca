using Api_Biblioteca.Datos;
using Api_Biblioteca.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Biblioteca.Controllers
{
    [ApiController]
    [Route("api/autores")] 
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Autor>> Get()
        {
            return await context.Autores.ToListAsync();
        }

        //FromRoute indica que le parametro proviene de la misma ruta//?incluirLibros=false|true
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> Get([FromRoute] int id, [FromQuery] bool incluirLibros)
        {
            var autor = await context.Autores
                .Include(x => x.Libros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(autor is null) 
            {
                return NotFound();
            }

            return autor;

        }
        //FromBody los parametros se envian por el cuerpo de la peticion
        [HttpPost]
        public async Task<ActionResult>Post([FromBody]Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Autor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest("Los ids deben de coincidir");
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrado = await context.Autores.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrado == 0)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
