using System;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class EntidadPropiedad : EntidadCreadoModificadoBorrado<Guid>
    {
        public Guid EntidadId { get; set; }
        public string Nombre { get; set; }
        public short PropiedadTipoId { get; set; }
        public string PropiedadTipoEspecificaciones { get; set; }
        public bool PermiteNull { get; set; }
        public bool EsCalculada { get; set; }

        public Entidad Entidad { get; set; }
        public PropiedadTipo Tipo { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
