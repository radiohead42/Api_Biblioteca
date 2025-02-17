using Microsoft.AspNetCore.Identity;

namespace Api_Biblioteca.Entidades
{
    public class Usuario: IdentityUser
    {
        public DateTime FechaNacimiento { get; set; }
    }
}
