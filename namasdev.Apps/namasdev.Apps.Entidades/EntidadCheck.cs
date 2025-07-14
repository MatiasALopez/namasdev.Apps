using System;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public class EntidadCheck : Entidad<Guid>
    {
        public Guid EntidadId { get; set; }
        public string Nombre { get; set; }
        public string Expresion { get; set; }

        public virtual Entidad Entidad { get; set; }
    }
}
