
namespace namasdev.Apps.Entidades.Valores
{
    public class EntidadPropiedades
    {
        public class Id
        {
            public const string ETIQUETA = "ID";
            public const int ORDEN = 1;
        }

        public class CreadoPor
        {
            public const int ORDEN = 901;
        }

        public class CreadoFecha
        {
            public const int ORDEN = 902;
        }

        public class UltimaModificacionPor
        {
            public const int ORDEN = 903;
        }

        public class UltimaModificacionFecha
        {
            public const int ORDEN = 904;
        }

        public class BorradoPor
        {
            public const int ORDEN = 905;
        }

        public class BorradoFecha
        {
            public const int ORDEN = 906;
        }

        public class Borrado
        {
            public const string CALCULADA_FORMULA_FORMATO = "ISNULL(CONVERT(bit,CASE WHEN [{0}] IS NULL THEN 0 ELSE 1 END), 0)";
            public const int ORDEN = 907;
        }
    }
}
