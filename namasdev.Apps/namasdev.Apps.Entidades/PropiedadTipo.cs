using namasdev.Core.Entity;
using namasdev.Core.Types;
using namasdev.Apps.Entidades.Valores;

namespace namasdev.Apps.Entidades
{
    public partial class PropiedadTipo : Entidad<short>
    {
        public string Nombre { get; set; }
        public string CLRType { get; set; }

        public bool CLRTypeEsNullable 
        {
            get { return Id != PropiedadTipos.TEXTO; }
        }

        public string CLRTypeNullable 
        {
            get 
            { 
                return CLRTypeEsNullable
                    ? $"{CLRType}?"
                    : CLRType;
            }
        }

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

                default:
                    return TSQLType;
            }
        }
    }
}
