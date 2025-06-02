namespace namasdev.Apps.Entidades.Metadata
{
    public class BajaTipoMetadata
    {
        public const string NOMBRE = "BajaTipo";
        public const string NOMBRE_PLURAL = "BajaTipos";

        public const string ETIQUETA = "Tipo de baja";
        public const string ETIQUETA_PLURAL = "Tipos de baja";

        public class BD
        {
            public const string TABLA = "BajaTipos";
            public const string ID = "BajaTipoId";
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
                public const int TAMAÑO_MAX = 50;
            }
        }
    }
}
