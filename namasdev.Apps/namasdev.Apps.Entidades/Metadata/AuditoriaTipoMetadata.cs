
namespace namasdev.Apps.Entidades.Metadata
{
    public class AuditoriaTipoMetadata
    {
        public const string NOMBRE = "AuditoriaTipo";
        public const string NOMBRE_PLURAL = "AuditoriaTipos";

        public const string ETIQUETA = "Tipo auditoría";
        public const string ETIQUETA_PLURAL = "Tipos auditoría";

        public class BD
        {
            public const string TABLA = "AuditoriaTipos";
            public const string ID = "AuditoriaTipoId";
        }

        public class Nombre
        {
            public const string ETIQUETA = "Nombre";
            public const int TAMAÑO_MAX = 100;
        }
    }
}
