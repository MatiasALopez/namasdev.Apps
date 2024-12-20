
namespace namasdev.Apps.Entidades.Metadata
{
    public class AuditoriaTipoMetadata
    {
        public const string NOMBRE = "Tipo auditoría";

        public class BD
        {
            public const string TABLA = "AuditoriaTipos";
            public const string ID = "AuditoriaTipoId";
        }

        public class Nombre
        {
            public const string DISPLAY_NAME = "Nombre";
            public const int TAMAÑO_MAX = 100;
        }
    }
}
