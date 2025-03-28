using System;

using namasdev.Core.Entity;
using namasdev.Apps.Entidades.Valores;
using namasdev.Core.Types;

namespace namasdev.Apps.Entidades
{
    public partial class EntidadPropiedad : EntidadCreadoModificadoBorrado<Guid>
    {
        public Guid EntidadId { get; set; }
        public string Nombre { get; set; }
        public string NombreOId
        {
            get { return EsId() ? "Id" : Nombre; }
        }

        private string _nombreCamelCase;
        public string NombreCamelCase
        {
            get { return _nombreCamelCase ?? (_nombreCamelCase = Nombre.ToFirstLetterLowercase()); }
        }

        public string Etiqueta { get; set; }
        public short PropiedadTipoId { get; set; }
        public string PropiedadTipoEspecificaciones { get; set; }
        public bool PermiteNull { get; set; }
        public short Orden { get; set; }
        public string CalculadaFormula { get; set; }
        public bool EsCalculada 
        {
            get { return !string.IsNullOrWhiteSpace(CalculadaFormula); }
        }

        public bool GeneradaAlCrear { get; set; }
        public bool Editable { get; set; }

        public Entidad Entidad { get; set; }
        public PropiedadTipo Tipo { get; set; }

        private PropiedadTipoEspecificacionesTexto _especificacionesTexto;
        public PropiedadTipoEspecificacionesTexto EspecificacionesTexto
        {
            get
            {
                return _especificacionesTexto ?? 
                    (_especificacionesTexto = 
                        PropiedadTipoId == PropiedadTipos.TEXTO
                        ? Newtonsoft.Json.JsonConvert.DeserializeObject<PropiedadTipoEspecificacionesTexto>(PropiedadTipoEspecificaciones)
                        : null);
            }
        }

        private PropiedadTipoEspecificacionesDecimal _especificacionesDecimal;
        public PropiedadTipoEspecificacionesDecimal EspecificacionesDecimal
        {
            get
            {
                return _especificacionesDecimal ??
                    (_especificacionesDecimal = 
                        PropiedadTipoId == PropiedadTipos.DECIMAL
                        ? Newtonsoft.Json.JsonConvert.DeserializeObject<PropiedadTipoEspecificacionesDecimal>(PropiedadTipoEspecificaciones)
                        : null);
            }
        }

        private PropiedadTipoEspecificacionesDecimalFlotante _especificacionesDecimalFlotante;
        public PropiedadTipoEspecificacionesDecimalFlotante EspecificacionesDecimalFlotante
        {
            get
            {
                return _especificacionesDecimalFlotante ??
                    (_especificacionesDecimalFlotante = 
                        PropiedadTipoId == PropiedadTipos.DECIMAL_FLOTANTE
                        ? Newtonsoft.Json.JsonConvert.DeserializeObject<PropiedadTipoEspecificacionesDecimalFlotante>(PropiedadTipoEspecificaciones)
                        : null);
            }
        }

        private PropiedadTipoEspecificacionesEntero _especificacionesEntero;
        public PropiedadTipoEspecificacionesEntero EspecificacionesEntero
        {
            get
            {
                return _especificacionesEntero ??
                    (_especificacionesEntero =
                        PropiedadTipoId == PropiedadTipos.ENTERO
                        ? Newtonsoft.Json.JsonConvert.DeserializeObject<PropiedadTipoEspecificacionesEntero>(PropiedadTipoEspecificaciones)
                        : null);
            }
        }

        private PropiedadTipoEspecificacionesEnteroCorto _especificacionesEnteroCorto;
        public PropiedadTipoEspecificacionesEnteroCorto EspecificacionesEnteroCorto
        {
            get
            {
                return _especificacionesEnteroCorto ??
                    (_especificacionesEnteroCorto =
                        PropiedadTipoId == PropiedadTipos.ENTERO_CORTO
                        ? Newtonsoft.Json.JsonConvert.DeserializeObject<PropiedadTipoEspecificacionesEnteroCorto>(PropiedadTipoEspecificaciones)
                        : null);
            }
        }

        private PropiedadTipoEspecificacionesEnteroLargo _especificacionesEnteroLargo;
        public PropiedadTipoEspecificacionesEnteroLargo EspecificacionesEnteroLargo
        {
            get
            {
                return _especificacionesEnteroLargo ??
                    (_especificacionesEnteroLargo = 
                        PropiedadTipoId == PropiedadTipos.ENTERO_LARGO
                        ? Newtonsoft.Json.JsonConvert.DeserializeObject<PropiedadTipoEspecificacionesEnteroLargo>(PropiedadTipoEspecificaciones)
                        : null);
            }
        }

        public override string ToString()
        {
            return Nombre;
        }

        public bool EsId()
        {
            return Nombre == IdNombre(this.Entidad);
        }

        public bool EsAuditoria()
        {
            return EsAuditoriaCreado()
                || EsAuditoriaUltimaModificacion()
                || EsAuditoriaBorrado();
        }

        public bool EsAuditoriaCreado()
        {
            return Nombre == EntidadPropiedades.CreadoPor.NOMBRE
                || Nombre == EntidadPropiedades.CreadoFecha.NOMBRE;
        }

        public bool EsAuditoriaUltimaModificacion()
        {
            return Nombre == EntidadPropiedades.UltimaModificacionPor.NOMBRE
                || Nombre == EntidadPropiedades.UltimaModificacionFecha.NOMBRE;
        }

        public bool EsAuditoriaBorrado()
        {
            return Nombre == EntidadPropiedades.BorradoPor.NOMBRE
                || Nombre == EntidadPropiedades.BorradoFecha.NOMBRE
                || Nombre == EntidadPropiedades.Borrado.NOMBRE;
        }

        public bool EsPropiedadDefault()
        {
            return EsId() || EsAuditoria();
        }

        public static string IdNombre(Entidad entidad)
        {
            return $"{entidad.Nombre}Id";
        }
    }
}
