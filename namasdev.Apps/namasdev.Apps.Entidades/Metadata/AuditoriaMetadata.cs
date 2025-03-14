namespace namasdev.Apps.Entidades.Metadata
{
    public class AuditoriaMetadata
    {
        public const string NOMBRE = "Auditoria";
        public const string NOMBRE_PLURAL = "Auditorias";

        public const string ETIQUETA = "Auditoría";
        public const string ETIQUETA_PLURAL = "Auditorías";

        public class BD
        {
            public const string TABLA = "Auditorias";
            public const string ID = "Id";
        }

        public class Propiedades
        {
            public class Tabla
            {
                public const string ETIQUETA = "Tabla";
                public const int TAMAÑO_MAX = 100;
            }

            public class Nombre
            {
                public const string ETIQUETA = "Nombre";
                public const int TAMAÑO_MAX = 100;
            }

            public class AuditoriaTipoId
            {
                public const string ETIQUETA = "Tipo";
            }

            public class Detalle
            {
                public const string ETIQUETA = "Detalle";
            }

            public class UserId
            {
                public const string ETIQUETA = "Usuario";
            }

            public class FechaHora
            {
                public const string ETIQUETA = "Fecha/Hora";
            }
        }

        public class Mensajes
        {
            public const string AGREGAR_OK = AuditoriaMetadata.ETIQUETA + " agregada correctamente.";
            public const string AGREGAR_ERROR = "No se pudo agregar la " + AuditoriaMetadata.ETIQUETA;

            public const string EDITAR_OK = AuditoriaMetadata.ETIQUETA + " actualizada correctamente.";
            public const string EDITAR_ERROR = "No se pudo actualizar la " + AuditoriaMetadata.ETIQUETA;

            public const string ELIMINAR_OK = AuditoriaMetadata.ETIQUETA + " eliminada correctamente.";
            public const string ELIMINAR_ERROR = "No se pudo eliminar la " + AuditoriaMetadata.ETIQUETA;
        }
    }
}
