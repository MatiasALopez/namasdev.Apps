namespace namasdev.Apps.Entidades.Metadata
{
    public class IdiomaEntidadPropiedadesMetadataMetadata
    {
        public const string NOMBRE = "IdiomaEntidadPropiedadesMetadata";
        public const string NOMBRE_PLURAL = "IdiomasEntidadPropiedadesMetadata";

        public const string ETIQUETA = "Metadata de propiedades de Idioma";
        public const string ETIQUETA_PLURAL = "Metadata de propiedades de Idioma";

        public class BD
        {
            public const string TABLA = "IdiomasEntidadPropiedadesMetadata";
            public const string ID = "IdiomaId";
        }

        public class Propiedades
        {
            public class IdiomaId
            {
                public const string ETIQUETA = "ID";
                public const int TAMAÑO_EXACTO = 2;
            }

            public class CreadoPorNombre
            {
                public const string ETIQUETA = "Creado por - Nombre";
                public const int TAMAÑO_MAX = 100;
            }

            public class CreadoPorEtiqueta
            {
                public const string ETIQUETA = "Creado por - Etiqueta";
                public const int TAMAÑO_MAX = 100;
            }

            public class CreadoFechaNombre
            {
                public const string ETIQUETA = "Creado fecha - Nombre";
                public const int TAMAÑO_MAX = 100;
            }

            public class CreadoFechaEtiqueta
            {
                public const string ETIQUETA = "Creado fecha - Etiqueta";
                public const int TAMAÑO_MAX = 100;
            }

            public class UltimaModificacionPorNombre
            {
                public const string ETIQUETA = "Ultima modificación por - Nombre";
                public const int TAMAÑO_MAX = 100;
            }

            public class UltimaModificacionPorEtiqueta
            {
                public const string ETIQUETA = "Ultima modificación por - Etiqueta";
                public const int TAMAÑO_MAX = 100;
            }

            public class UltimaModificacionFechaNombre
            {
                public const string ETIQUETA = "Ultima modificación fecha - Nombre";
                public const int TAMAÑO_MAX = 100;
            }

            public class UltimaModificacionFechaEtiqueta
            {
                public const string ETIQUETA = "Ultima modificación fecha - Etiqueta";
                public const int TAMAÑO_MAX = 100;
            }

            public class BorradoPorNombre
            {
                public const string ETIQUETA = "Borrado por - Nombre";
                public const int TAMAÑO_MAX = 100;
            }

            public class BorradoPorEtiqueta
            {
                public const string ETIQUETA = "Borrado por - Etiqueta";
                public const int TAMAÑO_MAX = 100;
            }

            public class BorradoFechaNombre
            {
                public const string ETIQUETA = "Borrado fecha - Nombre";
                public const int TAMAÑO_MAX = 100;
            }

            public class BorradoFechaEtiqueta
            {
                public const string ETIQUETA = "Borrado fecha - Etiqueta";
                public const int TAMAÑO_MAX = 100;
            }

            public class BorradoNombre
            {
                public const string ETIQUETA = "Borrado - Nombre";
                public const int TAMAÑO_MAX = 100;
            }

            public class BorradoEtiqueta
            {
                public const string ETIQUETA = "Borrado - Etiqueta";
                public const int TAMAÑO_MAX = 100;
            }
        }
    }
}
