using Microsoft.Extensions.Options;

namespace Api_Biblioteca
{
    public class PagosProcesamiento
    {
        public PagosProcesamiento(IOptionsMonitor<TarifaOpciones> opcionesMonitor)
        {
            _tarifasOpciones = opcionesMonitor;
        }
    }
}
