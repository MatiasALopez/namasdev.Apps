using System;
using System.Collections.Generic;

namespace namasdev.Apps.Negocio.DTO.EntidadesPropiedades
{
    public class EstablecerComoClaveParametros : ParametrosConUsuarioBase
    {
        public Guid EntidadId { get; set; } 
        public IEnumerable<Guid> PropiedadesIds { get; set; }
    }
}
