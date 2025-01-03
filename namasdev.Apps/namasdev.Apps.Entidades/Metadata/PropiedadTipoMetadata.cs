
namespace namasdev.Apps.Entidades.Metadata
{
    public class PropiedadTipoMetadata
    {
        public const string NOMBRE = "PropiedadTipo";
        public const string NOMBRE_PLURAL = "PropiedadTipos";

        public const string ETIQUETA = "Tipo de propiedad";
        public const string ETIQUETA_PLURAL = "Tipos de propiedad";

        public class BD
        {
            public const string TABLA = "PropiedadTipos";
            public const string ID = "PropiedadTipoId";
        }

        public class Nombre
        {
            public const string ETIQUETA = "Nombre";
            public const int TAMAÑO_MAX = 100;
        }
    }
}
