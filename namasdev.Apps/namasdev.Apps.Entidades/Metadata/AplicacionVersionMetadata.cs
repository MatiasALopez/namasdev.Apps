namespace namasdev.Apps.Entidades.Metadata
{
    public class AplicacionVersionMetadata
    {
        public const string NOMBRE = "Versión";

        public class BD
        {
            public const string TABLA = "AplicacionesVersiones";
            public const string ID = "AplicacionVersionId";
        }

        public class AplicacionId
        {
            public const string DISPLAY_NAME = "Aplicación";
        }

        public class Nombre
        {
            public const string DISPLAY_NAME = "Nombre";
            public const int TAMAÑO_MAX = 100;
        }
    }
}
