using System;
using System.Collections.Generic;
using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public class EntidadIndice : Entidad<Guid>
    {
        public Guid EntidadId { get; set; }
        public string Nombre { get; set; }
        public bool EsUnique { get; set; }

        public virtual Entidad Entidad { get; set; }
        public virtual List<EntidadIndicePropiedad> Propiedades { get; set; }
    }
}
