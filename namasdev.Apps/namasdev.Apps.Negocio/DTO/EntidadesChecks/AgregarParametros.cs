using System;

namespace namasdev.Apps.Negocio.DTO.EntidadesChecks
{
    public class AgregarParametros : ParametrosConUsuarioBase
    {
        public Guid EntidadId { get; set; }
        public string Nombre { get; set; }
        public string Expresion { get; set; }
    }
}
