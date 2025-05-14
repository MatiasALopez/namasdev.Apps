
using System;

namespace namasdev.Apps.Negocio.DTO.EntidadesIndicesPropiedades
{
    public class AgregarParametros : ParametrosConUsuarioBase
    {
        public Guid EntidadIndiceId { get; set; }
        public Guid EntidadPropiedadId { get; set; }
    }
}
