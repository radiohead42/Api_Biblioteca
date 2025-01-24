using Api_Biblioteca.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Api_Biblioteca.Controllers
{
    [ApiController]
    [Route("api/valores")]
    public class ValoresController: ControllerBase
    {
        private readonly IRepositorioValores repositorioValores;
        private readonly ServicioTransient servicioTransient1;
        private readonly ServicioTransient servicioTransient2;
        private readonly ServicioScope servicioScope1;
        private readonly ServicioScope servicioScope2;
        private readonly ServicioSingleton servicioSingleton;

        //Inyeccion de dependecias / vinculo devil
        //Los servicios se encargan de contruir las instancias de las clases
        public ValoresController(IRepositorioValores repositorioValores, 
            ServicioTransient servicioTransient1,
            ServicioTransient servicioTransient2,
            ServicioScope servicioScope1,
            ServicioScope servicioScope2,
            ServicioSingleton servicioSingleton)
        {
            this.repositorioValores = repositorioValores;
            this.servicioTransient1 = servicioTransient1;
            this.servicioTransient2 = servicioTransient2;
            this.servicioScope1 = servicioScope1;
            this.servicioScope2 = servicioScope2;
            this.servicioSingleton = servicioSingleton;
        }

        [HttpGet("servicio")]
        public IActionResult GetServicios()
        {
            return Ok(new
            {
                ServicioTransient = new
                {
                    servicioTransient1 = servicioTransient1.ObtenerGuid,
                    servicioTransient2 = servicioTransient2.ObtenerGuid
                },

                ServicioScope = new
                {
                    servicioScope1 = servicioScope1.ObtenerGuid,
                    servicioScope2 = servicioScope2.ObtenerGuid
                },

                ServicioSingleton = servicioSingleton.ObtenerGuid
            });
        }

        [HttpGet]
        public IEnumerable<Valor> Get()
        {
            return repositorioValores.ObtenerValores();
        }

        [HttpPost]
        public IActionResult Post(Valor valor)
        {
            repositorioValores.InsertarValor(valor);
            return Ok(valor);
        }
    }
}
