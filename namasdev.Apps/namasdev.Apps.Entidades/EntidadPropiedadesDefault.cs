using System;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class EntidadPropiedadesDefault : Entidad<Guid>
    {
        public short? IDPropiedadTipoId { get; set; }
        public bool AuditoriaCreado { get; set; }
        public bool AuditoriaUltimaModificacion { get; set; }
        public bool AuditoriaBorrado { get; set; }

        public virtual Entidad Entidad { get; set; }
        public virtual PropiedadTipo IDTipo { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
