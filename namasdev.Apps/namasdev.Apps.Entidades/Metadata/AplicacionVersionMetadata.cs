namespace namasdev.Apps.Entidades.Metadata
{
    public class AplicacionVersionMetadata
    {
        public const string NOMBRE = "Version";
        public const string NOMBRE_PLURAL = "Versiones";

        public const string ETIQUETA = "Versión";
        public const string ETIQUETA_PLURAL = "Versiones";

        public class BD
        {
            public const string TABLA = "AplicacionesVersiones";
            public const string ID = "AplicacionVersionId";
        }

        public class AplicacionId
        {
            public const string ETIQUETA = "Aplicación";
        }

        public class Nombre
        {
            public const string ETIQUETA = "Nombre";
            public const int TAMAÑO_MAX = 100;
        }

        public class AplicacionVersionIdBase
        {
            public const string ETIQUETA = "Versión base";
        }
    }
}
