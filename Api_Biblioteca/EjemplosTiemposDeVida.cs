namespace Api_Biblioteca
{
    public class ServicioTransient
    {
        private readonly Guid _id;
        public ServicioTransient()
        {
            _id = Guid.NewGuid();
        }
        public Guid ObtenerGuid => _id;
    }


    public class ServicioScope
    {
        private readonly Guid _id;
        public ServicioScope()
        {
            _id = Guid.NewGuid();
        }
        public Guid ObtenerGuid => _id;
    }


    public class ServicioSingleton
    {
        private readonly Guid _id;
        public ServicioSingleton()
        {
            _id = Guid.NewGuid();
        }
        public Guid ObtenerGuid => _id;
    }

}
