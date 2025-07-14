using System;

namespace namasdev.Apps.Negocio.DTO.EntidadesChecks
{
    public class ActualizarParametros : ParametrosEntidadBase<Guid>
    {
        public string Nombre { get; set; }
        public string Expresion { get; set; }
    }
}
