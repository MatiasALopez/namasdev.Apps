namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadCheckMetadata
    {
        public const string NOMBRE = "EntidadCheck";
        public const string NOMBRE_PLURAL = "EntidadesChecks";

        public const string ETIQUETA = "Check";
        public const string ETIQUETA_PLURAL = "Checks";

        public class BD
        {
            public const string TABLA = "EntidadesChecks";
            public const string ID = "EntidadCheckId";
        }

        public class Propiedades
        {
            public class Id
            {
                public const string ETIQUETA = "ID";
            }

            public class Nombre
            {
                public const string ETIQUETA = "Nombre";
                public const int TAMAÑO_MAX = 200;
            }

            public class ExpresionNombre
            {
                public const string ETIQUETA = "Expresión";
                public const int TAMAÑO_MAX = 2000;
            }
        }

        public class Mensajes
        {
            public const string AGREGAR_OK = EntidadCheckMetadata.ETIQUETA + " agregada correctamente.";
            public const string AGREGAR_ERROR = "No se pudo agregar la " + EntidadCheckMetadata.ETIQUETA;

            public const string EDITAR_OK = EntidadCheckMetadata.ETIQUETA + " actualizada correctamente.";
            public const string EDITAR_ERROR = "No se pudo actualizar la " + EntidadCheckMetadata.ETIQUETA;

            public const string ELIMINAR_OK = EntidadCheckMetadata.ETIQUETA + " eliminada correctamente.";
            public const string ELIMINAR_ERROR = "No se pudo eliminar la " + EntidadCheckMetadata.ETIQUETA;
            public const string ELIMINAR_CONFIRMACION = "¿Estás seguro que deseas eliminar la " + EntidadCheckMetadata.ETIQUETA + " seleccionada?";
        }
    }
}
