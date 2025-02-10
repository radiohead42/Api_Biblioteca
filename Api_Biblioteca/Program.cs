
//
using Api_Biblioteca;
using Api_Biblioteca.Datos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Area de servicios
//isntancia de repositorios valores quse se podra usar despues por inyeccion
//tambin solo se pueden poner los constructores de las clases

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
