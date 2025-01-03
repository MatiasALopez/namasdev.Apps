
namespace namasdev.Apps.Entidades.Metadata
{
    public class AplicacionMetadata
    {
        public const string NOMBRE = "Aplicacion";
        public const string NOMBRE_PLURAL = "Aplicaciones";

        public const string ETIQUETA = "Aplicación";
        public const string ETIQUETA_PLURAL = "Aplicaciones";

        public class BD
        {
            public const string TABLA = "Aplicaciones";
            public const string ID = "AplicacionId";
        }

        public class Nombre
        {
            public const string ETIQUETA = "Nombre";
            public const int TAMAÑO_MAX = 100;
        }
    }
}
