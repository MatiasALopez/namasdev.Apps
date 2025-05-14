using System;
using System.Collections.Generic;

namespace namasdev.Apps.Negocio.DTO.EntidadesIndices
{
    public class AgregarParametros : ParametrosConUsuarioBase
    {
        public Guid EntidadId { get; set; }
        public string Nombre { get; set; }
        public bool EsUnique { get; set; }
        public IEnumerable<Guid> EntidadesPropiedadesIds { get; set; }
    }
}
