﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api_Biblioteca.Controllers
{
    [ApiController]
    [Route("api/configuraciones")]
    public class ConfiguracionesController: ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IConfigurationSection seccion_01;
        private readonly IConfigurationSection seccion_02;
        private readonly PersonaOpciones _opcionesPersona;

        //IOptionSnapshot: Si actualiza los valores pero solo es un poco mas lenta
        public ConfiguracionesController(IConfiguration configuration, 
            IOptionsSnapshot<PersonaOpciones> opcionesPersona)
        {
            this.configuration = configuration;
            seccion_01 = configuration.GetSection("seccion_1");
            seccion_02 = configuration.GetSection("seccion_2");
            _opcionesPersona = opcionesPersona.Value;
        }

        [HttpGet("seccion_1_opciones")]
        public ActionResult GetSeccion10Opciones()
        {
            return Ok(_opcionesPersona);
        }

        [HttpGet("provedores")]
        public IActionResult GetProvedor()
        {
            var valor = configuration.GetValue<string>("quien_soy");
            return Ok(valor);
        }

        [HttpGet("obtenertodos")]
        public ActionResult GetObtenerTodos()
        {
            var hijos = configuration.GetChildren().Select(x => $"{x.Key}: {x.Value}");

            return Ok(new { hijos });
        }

        [HttpGet("seccion1")]
        public ActionResult GetSeccion01()
        {
            var nombre = seccion_01.GetValue<string>("nombre");

            var edad = seccion_01.GetValue<int>("edad");

            return Ok(new {nombre, edad});
        }

        [HttpGet("seccion2")]
        public ActionResult GetSeccion02()
        {
            var nombre = seccion_02.GetValue<string>("nombre");

            var edad = seccion_02.GetValue<int>("edad");

            return Ok(new { nombre, edad });
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            var opcion1 = configuration["apellido"];

            var opcion2 = configuration.GetValue<string>("apellido");

            return opcion2;
        }

        [HttpGet("secciones")]
        public ActionResult<string> GetSeccion()
        {
            var opcion1 = configuration["ConnectionStrings:DefaultConnection"];

            var opcion2 = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            var seccion = configuration.GetSection("ConnectionStrings");

            var opcion3 = seccion["DefaultConnection"];

            return opcion3;

        }
    }
}
