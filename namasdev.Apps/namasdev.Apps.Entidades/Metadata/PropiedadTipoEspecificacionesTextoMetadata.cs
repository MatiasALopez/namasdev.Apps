namespace namasdev.Apps.Entidades.Metadata
{
    public class PropiedadTipoEspecificacionesTextoMetadata
    {
        public const string NOMBRE = "EspecificacionesTexto";
        public const string ETIQUETA = "Especificaciones (Texto)";

        public class TamañoMinimo
        {
            public const string ETIQUETA = "Tamaño mínimo";
            public const int TAMAÑO_MAX = 10;
        }

        public class TamañoMaximo
        {
            public const string ETIQUETA = "Tamaño máximo";
            public const int TAMAÑO_MAX = 10;
        }

        public class TamañoExacto
        {
            public const string ETIQUETA = "Tamaño exacto";
            public const int TAMAÑO_MAX = 10;
        }

        public class RegEx
        {
            public const string ETIQUETA = "Expresión regular";
            public const int TAMAÑO_MAX = 100;
        }

        public class EsMultilinea
        {
            public const string ETIQUETA = "Multilínea";
        }
    }
}
