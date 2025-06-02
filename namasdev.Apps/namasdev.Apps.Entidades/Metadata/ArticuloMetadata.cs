namespace namasdev.Apps.Entidades.Metadata
{
    public class ArticuloMetadata
    {
        public const string NOMBRE = "Articulo";
        public const string NOMBRE_PLURAL = "Articulos";

        public const string ETIQUETA = "Artículo";
        public const string ETIQUETA_PLURAL = "Artículos";

        public class BD
        {
            public const string TABLA = "Articulos";
            public const string ID = "ArticuloId";
        }

        public class Propiedades
        {
            public class Nombre
            {
                public const string ETIQUETA = "Nombre";
                public const int TAMAÑO_MAX = 50;
            }
        }
    }
}
