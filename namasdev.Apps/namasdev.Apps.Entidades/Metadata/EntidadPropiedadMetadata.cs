namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadPropiedadMetadata
    {
        public const string NOMBRE = "Propiedad";

        public class BD
        {
            public const string TABLA = "EntidadesPropiedades";
            public const string ID = "EntidadPropiedadId";
        }

        public class EntidadId
        {
            public const string DISPLAY_NAME = "Entidad";
        }

        public class Nombre
        {
            public const string DISPLAY_NAME = "Nombre";
            public const int TAMAÑO_MAX = 100;
        }

        public class PropiedadTipoId
        {
            public const string DISPLAY_NAME = "Tipo";
        }

        public class PropiedadTipoEspecificaciones
        {
            public const string DISPLAY_NAME = "Tipo Especificaciones";
        }

        public class PermiteNull
        {
            public const string DISPLAY_NAME = "Permite null";
        }

        public class EsCalculada
        {
            public const string DISPLAY_NAME = "Es calculada";
        }
    }
}
