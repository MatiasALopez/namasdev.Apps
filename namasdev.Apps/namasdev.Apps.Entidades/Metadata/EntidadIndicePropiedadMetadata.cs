namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadIndicePropiedadMetadata
    {
        public const string NOMBRE = "EntidadIndicePropiedad";
        public const string NOMBRE_PLURAL = "EntidadesIndicesPropiedades";

        public const string ETIQUETA = "Propiedad de índice";
        public const string ETIQUETA_PLURAL = "Propiedades de índice";

        public class BD
        {
            public const string TABLA = "EntidadesIndicesPropiedades";
            public const string ID = "EntidadIndicePropiedadId";
        }

        public class Propiedades
        {
            public class EntidadIndiceId
            {
                public const string ETIQUETA = EntidadIndiceMetadata.ETIQUETA;
            }

            public class EntidadPropiedadId
            {
                public const string ETIQUETA = EntidadPropiedadMetadata.ETIQUETA;
            }
        }

        public class Mensajes
        {
            public const string AGREGAR_OK = EntidadIndicePropiedadMetadata.ETIQUETA + " agregada correctamente.";
            public const string AGREGAR_ERROR = "No se pudo agregar la " + EntidadIndicePropiedadMetadata.ETIQUETA;

            public const string EDITAR_OK = EntidadIndicePropiedadMetadata.ETIQUETA + " actualizada correctamente.";
            public const string EDITAR_ERROR = "No se pudo actualizar la " + EntidadIndicePropiedadMetadata.ETIQUETA;

            public const string ELIMINAR_OK = EntidadIndicePropiedadMetadata.ETIQUETA + " eliminada correctamente.";
            public const string ELIMINAR_ERROR = "No se pudo eliminar la " + EntidadIndicePropiedadMetadata.ETIQUETA;
        }
    }
}
