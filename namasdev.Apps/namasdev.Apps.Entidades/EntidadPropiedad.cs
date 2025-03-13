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

        public PropiedadTipoEspecificacionesTexto EspecificacionesTexto
        {
            get
            {
                return PropiedadTipoId == PropiedadTipos.TEXTO
                    ? Newtonsoft.Json.JsonConvert.DeserializeObject<PropiedadTipoEspecificacionesTexto>(PropiedadTipoEspecificaciones)
                    : null;
            }
        }

        public PropiedadTipoEspecificacionesDecimal EspecificacionesDecimal
        {
            get
            {
                return PropiedadTipoId == PropiedadTipos.DECIMAL
                    ? Newtonsoft.Json.JsonConvert.DeserializeObject<PropiedadTipoEspecificacionesDecimal>(PropiedadTipoEspecificaciones)
                    : null;
            }
        }

        public PropiedadTipoEspecificacionesDecimalLargo EspecificacionesDecimalLargo
        {
            get
            {
                return PropiedadTipoId == PropiedadTipos.DECIMAL_LARGO
                    ? Newtonsoft.Json.JsonConvert.DeserializeObject<PropiedadTipoEspecificacionesDecimalLargo>(PropiedadTipoEspecificaciones)
                    : null;
            }
        }

        public PropiedadTipoEspecificacionesEntero EspecificacionesEntero
        {
            get
            {
                return PropiedadTipoId == PropiedadTipos.ENTERO
                    ? Newtonsoft.Json.JsonConvert.DeserializeObject<PropiedadTipoEspecificacionesEntero>(PropiedadTipoEspecificaciones)
                    : null;
            }
        }

        public PropiedadTipoEspecificacionesEnteroLargo EspecificacionesEnteroLargo
        {
            get
            {
                return PropiedadTipoId == PropiedadTipos.ENTERO_LARGO
                    ? Newtonsoft.Json.JsonConvert.DeserializeObject<PropiedadTipoEspecificacionesEnteroLargo>(PropiedadTipoEspecificaciones)
                    : null;
            }
        }

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
