namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadEspecificacionesMetadata
    {
        public const string NOMBRE = "EntidadEspecificaciones";
        public const string NOMBRE_PLURAL = "EntidadesEspecificaciones";

        public const string ETIQUETA = "Especificaciones";
        public const string ETIQUETA_PLURAL = "Especificaciones";

        public class BD
        {
            public const string TABLA = "EntidadesEspecificaciones";
            public const string ID = "EntidadId";
        }

        public class Propiedades
        {
            public class Id
            {
                public const string ETIQUETA = "ID";
            }

            public class IdiomaArticuloId
            {
                public const string ETIQUETA = IdiomaArticuloMetadata.ETIQUETA;
                public const int TAMAÑO_EXACTO = 3;
            }

            public class IDPropiedadTipoId
            {
                public const string ETIQUETA = "Tipo de ID";
            }

            public class EsSoloLectura
            {
                public const string ETIQUETA = "Es sólo lectura";
            }

            public class BajaTipoId
            {
                public const string ETIQUETA = BajaTipoMetadata.ETIQUETA;
            }

            public class AuditoriaCreado
            {
                public const string ETIQUETA = "Auditoría creado";
            }

            public class AuditoriaUltimaModificacion
            {
                public const string ETIQUETA = "Auditoría última modificación";
            }

            public class AuditoriaBorrado
            {
                public const string ETIQUETA = "Auditoría borrado";
            }
        }

        public class Mensajes
        {
            public const string AGREGAR_OK = EntidadEspecificacionesMetadata.ETIQUETA + " agregadas correctamente.";
            public const string AGREGAR_ERROR = "No se pudieron agregar las " + EntidadEspecificacionesMetadata.ETIQUETA;

            public const string EDITAR_OK = EntidadEspecificacionesMetadata.ETIQUETA + " actualizada correctamente.";
            public const string EDITAR_ERROR = "No se pudieron actualizar las " + EntidadEspecificacionesMetadata.ETIQUETA;
        }
    }
}
