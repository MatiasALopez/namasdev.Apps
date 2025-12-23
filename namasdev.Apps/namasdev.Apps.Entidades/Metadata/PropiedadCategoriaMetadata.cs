namespace namasdev.Apps.Entidades.Metadata
{
    public class PropiedadCategoriaMetadata
    {
        public const string NOMBRE = "PropiedadCategoria";
        public const string NOMBRE_PLURAL = "PropiedadCategorias";

        public const string ETIQUETA = "Categoría de propiedad";
        public const string ETIQUETA_PLURAL = "Categorías de propiedad";

        public class BD
        {
            public const string TABLA = "PropiedadCategorias";
            public const string ID = "PropiedadCategoriaId";
        }

        public class Propiedades
        {
            public class Id
            {
                public const string ETIQUETA = "ID";
            }

            public class Nombre
            {
                public const string ETIQUETA = "Nombre";
                public const int TAMAÑO_MAX = 50;
            }
        }
    }
}
