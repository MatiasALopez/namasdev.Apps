
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

        public class Propiedades
        {
            public class Nombre
            {
                public const string ETIQUETA = "Nombre";
                public const int TAMAÑO_MAX = 100;
            }
        }

        public class Mensajes
        {
            public const string AGREGAR_OK = AplicacionMetadata.ETIQUETA + " agregada correctamente.";
            public const string AGREGAR_ERROR = "No se pudo agregar la " + AplicacionMetadata.ETIQUETA;

            public const string EDITAR_OK = AplicacionMetadata.ETIQUETA + " actualizada correctamente.";
            public const string EDITAR_ERROR = "No se pudo actualizar la " + AplicacionMetadata.ETIQUETA;

            public const string ELIMINAR_OK = AplicacionMetadata.ETIQUETA + " eliminada correctamente.";
            public const string ELIMINAR_ERROR = "No se pudo eliminar la " + AplicacionMetadata.ETIQUETA;
            public const string ELIMINAR_CONFIRMACION = "¿Estás seguro que deseas eliminar la " + AplicacionMetadata.ETIQUETA + " seleccionada?";
        }
    }
}
