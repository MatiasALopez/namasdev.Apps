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

        public class Propiedades
        {
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

        public class Mensajes
        {
            public const string AGREGAR_OK = AplicacionVersionMetadata.ETIQUETA + " agregada correctamente.";
            public const string AGREGAR_ERROR = "No se pudo agregar la " + AplicacionVersionMetadata.ETIQUETA;

            public const string EDITAR_OK = AplicacionVersionMetadata.ETIQUETA + " actualizada correctamente.";
            public const string EDITAR_ERROR = "No se pudo actualizar la " + AplicacionVersionMetadata.ETIQUETA;

            public const string ELIMINAR_OK = AplicacionVersionMetadata.ETIQUETA + " eliminada correctamente.";
            public const string ELIMINAR_ERROR = "No se pudo eliminar la " + AplicacionVersionMetadata.ETIQUETA;
        }
    }
}
