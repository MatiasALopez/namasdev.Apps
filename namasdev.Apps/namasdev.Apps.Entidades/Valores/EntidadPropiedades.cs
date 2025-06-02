
namespace namasdev.Apps.Entidades.Valores
{
    public class EntidadPropiedades
    {
        public class Id
        {
            public const string ETIQUETA = "ID";
        }

        public class CreadoPor
        {
            public const string NOMBRE = "CreadoPor";
            public const string ETIQUETA = "CreadoFecha";
            public const int ORDEN = 101;
        }

        public class CreadoFecha
        {
            public const string NOMBRE = "CreadoFecha";
            public const string ETIQUETA = "Fecha/hora creación";
            public const int ORDEN = 102;
        }

        public class UltimaModificacionPor
        {
            public const string NOMBRE = "UltimaModificacionPor";
            public const string ETIQUETA = "Usuario última modificación";
            public const int ORDEN = 103;
        }

        public class UltimaModificacionFecha
        {
            public const string NOMBRE = "UltimaModificacionFecha";
            public const string ETIQUETA = "Fecha/hora última modificación";
            public const int ORDEN = 104;
        }

        public class BorradoPor
        {
            public const string NOMBRE = "BorradoPor";
            public const string ETIQUETA = "Usuario borrado";
            public const int ORDEN = 105;
        }

        public class BorradoFecha
        {
            public const string NOMBRE = "BorradoFecha";
            public const string ETIQUETA = "Fecha/hora borrado";
            public const int ORDEN = 106;
        }

        public class Borrado
        {
            public const string NOMBRE = "Borrado";
            public const string ETIQUETA = "Borrado";
            public const string CALCULADA_FORMULA = "ISNULL(CONVERT(bit,CASE WHEN [BorradoFecha] IS NULL THEN 0 ELSE 1 END), 0)";
            public const int ORDEN = 107;
        }
    }
}
