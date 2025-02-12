using Api_Biblioteca.Datos;
using Api_Biblioteca.DTOs;
using Api_Biblioteca.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Biblioteca.Controllers
{
    [ApiController]
    [Route("api/autores-coleccion")]
    public class AutoresColeccionController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutoresColeccionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{ids}", Name = "ObtenerAutoresPorIds")]
        public async Task<ActionResult<List<AutorConLibrosDTO>>> Get(string ids)
        {
            var idsColeccion = new List<int>();

            foreach (var id in ids.Split(","))
            {
                if (int.TryParse(id, out int idint))
                {
                    idsColeccion.Add(idint);
                }
            }

            if(!idsColeccion.Any())
            {
                ModelState.AddModelError(nameof(ids), "Ningun Id fue encontrado");
                return ValidationProblem();
            }

            var autores = await context.Autores
                .Include(x => x.Libros)
                    .ThenInclude(x => x.Libro)
                .Where(x => idsColeccion.Contains(x.Id))
                .ToListAsync();

            if(autores.Count != idsColeccion.Count)
            {
                return NotFound();
            }

            var autoresDTO = mapper.Map<List<AutorConLibrosDTO>>(autores);  

            return autoresDTO;
        }


        [HttpPost]
        public async Task<ActionResult> Post(IEnumerable<AutorCreacionDTO> autorCreacionDTOs)
        {
            var autores = mapper.Map<IEnumerable<Autor>>(autorCreacionDTOs);

            context.AddRange(autores);

            await context.SaveChangesAsync();

            var autoresDTO =mapper.Map<IEnumerable<AutorDTO>>(autores);
            var ids = autores.Select(x => x.Id);

            var idsString = string.Join(",", ids);

            return CreatedAtRoute("ObtenerAutoresPorIds", new {ids = idsString}, autoresDTO);

        }
    }
}
