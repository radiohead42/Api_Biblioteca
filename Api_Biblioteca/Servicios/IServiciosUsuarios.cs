using Api_Biblioteca.Entidades;
using Microsoft.AspNetCore.Identity;

namespace Api_Biblioteca.Servicios
{
    public interface IServiciosUsuarios
    {
        Task<Usuario?> ObtenerUsuario();
    }
}