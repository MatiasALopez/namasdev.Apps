using System;

using namasdev.Core.Entity;
using namasdev.Apps.Entidades.Valores;

namespace namasdev.Apps.Entidades
{
    public partial class EntidadPropiedad : EntidadCreadoModificadoBorrado<Guid>
    {
        public Guid EntidadId { get; set; }
        public string Nombre { get; set; }
        public string Etiqueta { get; set; }
        public short PropiedadTipoId { get; set; }
        public string PropiedadTipoEspecificaciones { get; set; }
        public bool PermiteNull { get; set; }
        public short Orden { get; set; }
        public string CalculadaFormula { get; set; }
        public bool GeneradaAlCrear { get; set; }
        public bool Editable { get; set; }

        public Entidad Entidad { get; set; }
        public PropiedadTipo Tipo { get; set; }

        public override string ToString()
        {
            return Nombre;
        }

        public bool EsPropiedadDefault()
        {
            return Nombre == EntidadPropiedades.Id.Nombre(Entidad)
                || Nombre == EntidadPropiedades.CreadoPor.NOMBRE
                || Nombre == EntidadPropiedades.CreadoFecha.NOMBRE
                || Nombre == EntidadPropiedades.UltimaModificacionPor.NOMBRE
                || Nombre == EntidadPropiedades.UltimaModificacionFecha.NOMBRE
                || Nombre == EntidadPropiedades.BorradoPor.NOMBRE
                || Nombre == EntidadPropiedades.BorradoFecha.NOMBRE
                || Nombre == EntidadPropiedades.Borrado.NOMBRE;
        }
    }
}
