using System;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public class EntidadClave : Entidad<Guid>
    {
        public Guid EntidadId { get; set; }
        public Guid EntidadPropiedadId { get; set; }

        public virtual Entidad Entidad { get; set; }
        public virtual EntidadPropiedad Propiedad { get; set; }
    }
}
