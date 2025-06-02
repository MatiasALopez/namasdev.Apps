using System;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class EntidadEspecificaciones : Entidad<Guid>
    {
        public short? IDPropiedadTipoId { get; set; }
        public short ArticuloId { get; set; }
        public bool EsSoloLectura { get; set; }
        public short BajaTipoId { get; set; }
        public bool AuditoriaCreado { get; set; }
        public bool AuditoriaUltimaModificacion { get; set; }

        public virtual Entidad Entidad { get; set; }
        public virtual PropiedadTipo IDTipo { get; set; }
        public virtual Articulo Articulo { get; set; }
        public virtual BajaTipo BajaTipo { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
