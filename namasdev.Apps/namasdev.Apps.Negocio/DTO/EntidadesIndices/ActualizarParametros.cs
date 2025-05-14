using System;
using System.Collections.Generic;

namespace namasdev.Apps.Negocio.DTO.EntidadesIndices
{
    public class ActualizarParametros : ParametrosEntidadBase<Guid>
    {
        public string Nombre { get; set; }
        public bool EsUnique { get; set; }
        public IEnumerable<Guid> EntidadesPropiedadesIds { get; set; }
    }
}
