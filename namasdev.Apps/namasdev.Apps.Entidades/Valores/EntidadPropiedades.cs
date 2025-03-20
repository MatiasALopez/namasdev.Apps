
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
        }

        public class CreadoFecha
        {
            public const string NOMBRE = "CreadoFecha";
            public const string ETIQUETA = "Fecha/hora creación";
        }

        public class UltimaModificacionPor
        {
            public const string NOMBRE = "UltimaModificacionPor";
            public const string ETIQUETA = "Usuario última modificación";
        }

        public class UltimaModificacionFecha
        {
            public const string NOMBRE = "UltimaModificacionFecha";
            public const string ETIQUETA = "Fecha/hora última modificación";
        }

        public class BorradoPor
        {
            public const string NOMBRE = "BorradoPor";
            public const string ETIQUETA = "Usuario borrado";
        }

        public class BorradoFecha
        {
            public const string NOMBRE = "BorradoFecha";
            public const string ETIQUETA = "Fecha/hora borrado";
        }

        public class Borrado
        {
            public const string NOMBRE = "Borrado";
            public const string ETIQUETA = "Borrado";
            public const string CALCULADA_FORMULA = "ISNULL(CONVERT(bit,CASE WHEN [BorradoFecha] IS NULL THEN 0 ELSE 1 END), 0)";
        }
    }
}
