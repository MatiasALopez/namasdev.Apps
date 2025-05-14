using System;
using System.Collections.Generic;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class EntidadIndicePropiedad : Entidad<Guid>
    {
        public Guid EntidadIndiceId { get; set; }
        public Guid EntidadPropiedadId { get; set; }
            
        public virtual EntidadIndice Indice { get; set; }
        public virtual EntidadPropiedad Propiedad { get; set; }
    }
}
