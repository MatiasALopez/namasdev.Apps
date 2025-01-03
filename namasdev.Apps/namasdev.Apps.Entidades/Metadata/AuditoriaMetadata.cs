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
}
