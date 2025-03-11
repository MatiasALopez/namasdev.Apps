namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadIndiceMetadata
    {
        public const string NOMBRE = "EntidadIndice";
        public const string NOMBRE_PLURAL = "EntidadesIndices";

        public const string ETIQUETA = "Índice";
        public const string ETIQUETA_PLURAL = "Indices";

        public class BD
        {
            public const string TABLA = "EntidadesIndices";
            public const string ID = "EntidadIndiceId";
        }
        
        public class EntidadId
        {
            public const string ETIQUETA = EntidadMetadata.ETIQUETA;
        }

        public class Nombre
        {
            public const string ETIQUETA = "Nombre";
            public const int TAMAÑO_MAX = 100;
            public const string REG_EX = @"^[a-zA-Z][a-zA-ZñÑ0-9_]*$";
        }

        public class EsUnique
        {
            public const string ETIQUETA = EntidadMetadata.ETIQUETA;
        }
    }
}
