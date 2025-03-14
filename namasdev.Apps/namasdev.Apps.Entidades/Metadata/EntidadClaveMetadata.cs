namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadClaveMetadata
    {
        public const string NOMBRE = "EntidadClave";
        public const string NOMBRE_PLURAL = "EntidadesClaves";

        public const string ETIQUETA = "Clave";
        public const string ETIQUETA_PLURAL = "Claves";

        public class BD
        {
            public const string TABLA = "EntidadesClaves";
            public const string ID = "EntidadClaveId";
        }

        public class Propiedades
        {
            public class EntidadId
            {
                public const string ETIQUETA = EntidadMetadata.ETIQUETA;
            }

            public class EntidadPropiedadId
            {
                public const string ETIQUETA = EntidadPropiedadMetadata.ETIQUETA;
            }
        }

        public class Mensajes
        {
            public const string AGREGAR_OK = EntidadClaveMetadata.ETIQUETA + " agregada correctamente.";
            public const string AGREGAR_ERROR = "No se pudo agregar la " + EntidadClaveMetadata.ETIQUETA;

            public const string EDITAR_OK = EntidadClaveMetadata.ETIQUETA + " actualizada correctamente.";
            public const string EDITAR_ERROR = "No se pudo actualizar la " + EntidadClaveMetadata.ETIQUETA;

            public const string ELIMINAR_OK = EntidadClaveMetadata.ETIQUETA + " eliminada correctamente.";
            public const string ELIMINAR_ERROR = "No se pudo eliminar la " + EntidadClaveMetadata.ETIQUETA;
        }
    }
}
