
namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadMetadata
    {
        public const string NOMBRE = "Entidad";

        public class BD
        {
            public const string TABLA = "Entidades";
            public const string ID = "EntidadId";
        }

        public class AplicacionVersionId
        {
            public const string DISPLAY_NAME = "Versión aplicación";
        }

        public class Nombre
        {
            public const string DISPLAY_NAME = "Nombre";
            public const int TAMAÑO_MAX = 100;
        }
    }
}
