namespace namasdev.Apps.Entidades.Metadata
{
    public class AuditoriaMetadata
    {
        public const string NOMBRE = "Auditoría";

        public class BD
        {
            public const string TABLA = "Auditorias";
            public const string ID = "Id";
        }

        public class Tabla
        {
            public const string DISPLAY_NAME = "Tabla";
            public const int TAMAÑO_MAX = 100;
        }

        public class Nombre
        {
            public const string DISPLAY_NAME = "Nombre";
            public const int TAMAÑO_MAX = 100;
        }

        public class AuditoriaTipoId
        {
            public const string DISPLAY_NAME = "Tipo";
        }

        public class Detalle
        {
            public const string DISPLAY_NAME = "Detalle";
        }

        public class UserId
        {
            public const string DISPLAY_NAME = "Usuario";
        }

        public class FechaHora
        {
            public const string DISPLAY_NAME = "Fecha/Hora";
        }
    }
}
