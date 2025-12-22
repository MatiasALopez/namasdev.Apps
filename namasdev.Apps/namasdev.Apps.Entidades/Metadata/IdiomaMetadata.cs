namespace namasdev.Apps.Entidades.Metadata
{
    public class IdiomaMetadata
    {
        public const string NOMBRE = "Idioma";
        public const string NOMBRE_PLURAL = "Idiomas";

        public const string ETIQUETA = "Idioma";
        public const string ETIQUETA_PLURAL = "Idiomas";

        public class BD
        {
            public const string TABLA = "Idiomas";
            public const string ID = "IdiomaId";
        }

        public class Propiedades
        {
            public class IdiomaId
            {
                public const string ETIQUETA = "ID";
                public const int TAMAÑO_EXACTO = 2;
            }

            public class Nombre
            {
                public const string ETIQUETA = "Nombre";
                public const int TAMAÑO_MAX = 100;
            }
        }
    }
}
