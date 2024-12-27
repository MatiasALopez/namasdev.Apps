namespace namasdev.Apps.Entidades.Metadata
{
    public class PropiedadTipoEspecificacionesTextoMetadata
    {
        public const string NOMBRE = "Especificaciones (Texto)";

        public class TamañoMinimo
        {
            public const string DISPLAY_NAME = "Tamaño mínimo";
            public const int TAMAÑO_MAX = 10;
        }

        public class TamañoMaximo
        {
            public const string DISPLAY_NAME = "Tamaño máximo";
            public const int TAMAÑO_MAX = 10;
        }

        public class TamañoExacto
        {
            public const string DISPLAY_NAME = "Tamaño exacto";
            public const int TAMAÑO_MAX = 10;
        }

        public class RegEx
        {
            public const string DISPLAY_NAME = "Expresión regular";
            public const int TAMAÑO_MAX = 100;
        }
    }
}
