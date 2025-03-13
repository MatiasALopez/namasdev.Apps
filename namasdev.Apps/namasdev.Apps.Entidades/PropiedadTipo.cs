using namasdev.Core.Entity;
using namasdev.Core.Types;
using Newtonsoft.Json;

namespace namasdev.Apps.Entidades
{
    public partial class PropiedadTipo : Entidad<short>
    {
        public string Nombre { get; set; }
        public string CLRType { get; set; }
        public string TSQLType { get; set; }

        public override string ToString()
        {
            return Nombre;
        }

        public string TSQLTypeConEspecificaciones(EntidadPropiedad propiedad)
        {
            switch (Id)
            {
                case PropiedadTipos.TEXTO:
                    var especificacionesTexto = propiedad.EspecificacionesTexto;
                    return $"{TSQLType}({System.Convert.ToString(especificacionesTexto.TamañoMaximo ?? especificacionesTexto.TamañoExacto).ValueNotEmptyOrNull(valorNull: "max")})";

                case PropiedadTipos.DECIMAL:
                    var especificacionesDecimal = propiedad.EspecificacionesDecimal;
                    return $"{TSQLType}({especificacionesDecimal.DigitosEnteros+especificacionesDecimal.DigitosDecimales},{especificacionesDecimal.DigitosDecimales})";

                case PropiedadTipos.DECIMAL_LARGO:
                    var especificacionesDecimalLargo = propiedad.EspecificacionesDecimalLargo;
                    return $"{TSQLType}({especificacionesDecimalLargo.DigitosEnteros + especificacionesDecimalLargo.DigitosDecimales},{especificacionesDecimalLargo.DigitosDecimales})";

                default:
                    return TSQLType;
            }
        }
    }
}
