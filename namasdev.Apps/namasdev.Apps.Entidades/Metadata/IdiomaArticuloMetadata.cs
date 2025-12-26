namespace namasdev.Apps.Entidades.Metadata
{
    public class IdiomaArticuloMetadata
    {
        public const string NOMBRE = "IdiomaArticulo";
        public const string NOMBRE_PLURAL = "IdiomasArticulos";

        public const string ETIQUETA = "Artículo";
        public const string ETIQUETA_PLURAL = "Artículos";

        public class BD
        {
            public const string TABLA = "IdiomasArticulos";
            public const string ID = "IdiomaArticuloId";
        }

        public class Propiedades
        {
            public class Id
            {
                public const string ETIQUETA = "ID";
                public const int TAMAÑO_EXACTO = 3;
            }

            public class IdiomaId
            {
                public const string ETIQUETA = "Idioma";
                public const int TAMAÑO_EXACTO = 2;
            }

            public class Nombre
            {
                public const string ETIQUETA = "Nombre";
                public const int TAMAÑO_MAX = 50;
            }
        }
    }
}
