
namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadMetadata
    {
        public const string NOMBRE = "Entidad";
        public const string NOMBRE_PLURAL = "Entidades";

        public const string ETIQUETA = "Entidad";
        public const string ETIQUETA_PLURAL = "Entidades";

        public class BD
        {
            public const string TABLA = "Entidades";
            public const string ID = "EntidadId";
        }

        public class AplicacionVersionId
        {
            public const string ETIQUETA = AplicacionVersionMetadata.ETIQUETA;
        }

        public class Nombre
        {
            public const string ETIQUETA = "Nombre";
            public const int TAMAÑO_MAX = 100;
        }

        public class NombrePlural
        {
            public const string ETIQUETA = "Nombre plural";
            public const int TAMAÑO_MAX = 120;
        }

        public class Etiqueta
        {
            public const string ETIQUETA = "Etiqueta";
            public const int TAMAÑO_MAX = 120;
        }

        public class EtiquetaPlural
        {
            public const string ETIQUETA = "Etiqueta plural";
            public const int TAMAÑO_MAX = 140;
        }
    }
}
