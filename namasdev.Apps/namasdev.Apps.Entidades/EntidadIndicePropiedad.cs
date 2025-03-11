using System;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public class EntidadIndicePropiedad : Entidad<Guid>
    {
        public Guid EntidadIndiceId { get; set; }
        public Guid EntidadPropiedadId { get; set; }

        public virtual EntidadIndice Indice { get; set; }
        public virtual EntidadPropiedad Propiedad { get; set; }
    }
}
