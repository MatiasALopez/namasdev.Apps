using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Valores;

namespace namasdev.Apps.Negocio.DTO.EntidadesPropiedades
{
    public class EntidadParametrosBase : ParametrosConUsuarioBase
    {
        public string Nombre { get; set; }
        public string Etiqueta { get; set; }
        public short PropiedadTipoId { get; set; }
        
        public PropiedadTipoEspecificacionesTexto EspecificacionesTexto { get; set; }
        public PropiedadTipoEspecificacionesEntero EspecificacionesEntero { get; set; }
        public PropiedadTipoEspecificacionesDecimal EspecificacionesDecimal { get; set; }
        public IPropiedadTipoEspecificaciones EspecificacionesSegunTipo 
        {
            get
            {
                switch (PropiedadTipoId)
                {
                    case PropiedadTipos.TEXTO:
                        return EspecificacionesTexto;

                    case PropiedadTipos.ENTERO:
                    case PropiedadTipos.ENTERO_CORTO:
                    case PropiedadTipos.ENTERO_LARGO:
                        return EspecificacionesEntero;

                    case PropiedadTipos.DECIMAL:
                    case PropiedadTipos.DECIMAL_FLOTANTE:
                        return EspecificacionesDecimal;

                    default:
                        return null;
                }
            }
        }

        public bool PermiteNull { get; set; }
        public string CalculadaFormula { get; set; }
        public bool GeneradaAlCrear { get; set; }
        public bool Editable { get; set; }
    }
}
