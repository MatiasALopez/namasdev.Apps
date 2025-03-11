namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadClaveMetadata
    {
        public const string NOMBRE = "EntidadClave";
        public const string NOMBRE_PLURAL = "EntidadesClaves";

        public const string ETIQUETA = "Clave";
        public const string ETIQUETA_PLURAL = "Claves";

        public class BD
        {
            public const string TABLA = "EntidadesClaves";
            public const string ID = "EntidadClaveId";
        }

        public class EntidadId
        {
            public const string ETIQUETA = EntidadMetadata.ETIQUETA;
        }

        public class EntidadPropiedadId
        {
            public const string ETIQUETA = EntidadPropiedadMetadata.ETIQUETA;
        }
    }
}
