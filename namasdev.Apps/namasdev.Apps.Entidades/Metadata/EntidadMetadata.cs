
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

        public class Propiedades
        {
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

        public class Mensajes
        {
            public const string AGREGAR_OK = EntidadMetadata.ETIQUETA + " agregada correctamente.";
            public const string AGREGAR_ERROR = "No se pudo agregar la " + EntidadMetadata.ETIQUETA;

            public const string EDITAR_OK = EntidadMetadata.ETIQUETA + " actualizada correctamente.";
            public const string EDITAR_ERROR = "No se pudo actualizar la " + EntidadMetadata.ETIQUETA;

            public const string ELIMINAR_OK = EntidadMetadata.ETIQUETA + " eliminada correctamente.";
            public const string ELIMINAR_ERROR = "No se pudo eliminar la " + EntidadMetadata.ETIQUETA;
        }
    }
}
