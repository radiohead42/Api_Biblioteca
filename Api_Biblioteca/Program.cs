
//
using Api_Biblioteca;
using Api_Biblioteca.Datos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var diccionarioConfiguraciones = new Dictionary<string, string>
{
    {"quien_soy", "Un diccionario en memoria" }
};
//configuration se usa para agregar un nuevo provedor de configuracion / basicamente nueva forma de
//agregar variables de entorno
builder.Configuration.AddInMemoryCollection(diccionarioConfiguraciones!);

//Area de servicios
//isntancia de repositorios valores quse se podra usar despues por inyeccion
//tambin solo se pueden poner los constructores de las clases

//relaciona la clase con la seccion y sus campos
builder.Services.AddOptions<PersonaOpciones>()
    .Bind(builder.Configuration.GetSection(PersonaOpciones.Seccion))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<TarifaOpciones>()
    .Bind(builder.Configuration.GetSection(TarifaOpciones.Seccion))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddNewtonsoftJson();
    //.AddJsonOptions(opciones =>
    //opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//Toma el archivo ApplciationDbContext y pasa valores al contructor
builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
            opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

//Area de middlewares

app.MapControllers();

app.Run();
