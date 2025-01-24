using Api_Biblioteca.Entidades;

namespace Api_Biblioteca
{
    public interface IRepositorioValores
    {
        void InsertarValor(Valor valor);
        IEnumerable<Valor>ObtenerValores();
    }
}
