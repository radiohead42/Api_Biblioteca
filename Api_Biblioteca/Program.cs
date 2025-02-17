
//
using Api_Biblioteca;
using Api_Biblioteca.Datos;
using Api_Biblioteca.Entidades;
using Api_Biblioteca.Servicios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Area de servicios
//isntancia de repositorios valores quse se podra usar despues por inyeccion
//tambin solo se pueden poner los constructores de las clases

//relaciona la clase con la seccion y sus campos
//builder.Services.AddOptions<PersonaOpciones>()
//    .Bind(builder.Configuration.GetSection(PersonaOpciones.Seccion))
//    .ValidateDataAnnotations()
//    .ValidateOnStart();

//builder.Services.AddOptions<TarifaOpciones>()
//    .Bind(builder.Configuration.GetSection(TarifaOpciones.Seccion))
//    .ValidateDataAnnotations()
//    .ValidateOnStart();

//builder.Services.AddSingleton<PagosProcesamiento>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddNewtonsoftJson();
    //.AddJsonOptions(opciones =>
    //opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//Toma el archivo ApplciationDbContext y pasa valores al contructor
builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
            opciones.UseSqlServer("name=DefaultConnection"));

//IdentityUser: Es una clase que representa un usuario
builder.Services.AddIdentityCore<Usuario>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<Usuario>>();
builder.Services.AddScoped<SignInManager<Usuario>>();

builder.Services.AddTransient<IServiciosUsuarios, ServiciosUsuarios>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication().AddJwtBearer(opciones =>
{
    opciones.MapInboundClaims = false;

    //Cosas a tener en cuenta al validar el token
    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["llavejwt"]!)),
        ClockSkew = TimeSpan.Zero
    };

});

builder.Services.AddAuthorization(opciones =>
{
    opciones.AddPolicy("esadmin", politica => politica.RequireClaim("esadmin"));
});

var app = builder.Build();

//Area de middlewares

app.MapControllers();

app.Run();
