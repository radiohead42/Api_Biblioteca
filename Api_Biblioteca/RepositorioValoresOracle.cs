using Api_Biblioteca.Entidades;

namespace Api_Biblioteca
{
    public class RepositorioValoresOracle : IRepositorioValores
    {
        private List<Valor> _valores;

        public RepositorioValoresOracle()
        {
            _valores = new List<Valor>
            {
                new Valor{Id = 3, Nombre = "valor Oracle"},
                new Valor{Id = 4, Nombre = "valor Oracle 4"},
                new Valor{Id = 5, Nombre = "valor Oracle 5"},
            };
        }

        public IEnumerable<Valor> ObtenerValores()
        {
            return _valores;
        }
        public void InsertarValor(Valor valor)
        {
            _valores.Add(valor);
        }
    }
}
