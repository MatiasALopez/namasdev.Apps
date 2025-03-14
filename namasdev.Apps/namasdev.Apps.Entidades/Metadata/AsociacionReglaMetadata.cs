
namespace namasdev.Apps.Entidades.Metadata
{
    public class AsociacionReglaMetadata
    {
        public const string NOMBRE = "AsociacionRegla";
        public const string NOMBRE_PLURAL = "AsociacionReglas";

        public const string ETIQUETA = "Regla de Asociación";
        public const string ETIQUETA_PLURAL = "Reglas de Asociación";

        public class BD
        {
            public const string TABLA = "AsociacionReglas";
            public const string ID = "AsociacionReglaId";
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
