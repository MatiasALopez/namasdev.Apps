namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadIndicePropiedadMetadata
    {
        public const string NOMBRE = "EntidadIndicePropiedad";
        public const string NOMBRE_PLURAL = "EntidadesIndicesPropiedades";

        public const string ETIQUETA = "Propiedad de índice";
        public const string ETIQUETA_PLURAL = "Propiedades de índice";

        public class BD
        {
            public const string TABLA = "EntidadesIndicesPropiedades";
            public const string ID = "EntidadIndicePropiedadId";
        }

        public class EntidadIndiceId
        {
            public const string ETIQUETA = EntidadIndiceMetadata.ETIQUETA;
        }

        public class EntidadPropiedadId
        {
            public const string ETIQUETA = EntidadPropiedadMetadata.ETIQUETA;
        }
    }
}
