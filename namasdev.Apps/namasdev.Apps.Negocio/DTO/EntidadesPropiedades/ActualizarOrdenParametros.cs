using System;
using System.Collections.Generic;

namespace namasdev.Apps.Negocio.DTO.EntidadesPropiedades
{
    public class ActualizarOrdenParametros : ParametrosConUsuarioBase
    {
        public Guid EntidadId { get; set; } 
        public Dictionary<Guid, short> PropiedadesIdsYOrdenes { get; set; }
    }
}
