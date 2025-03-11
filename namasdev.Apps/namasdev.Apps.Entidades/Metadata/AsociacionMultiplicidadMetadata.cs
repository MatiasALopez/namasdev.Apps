
namespace namasdev.Apps.Entidades.Metadata
{
    public class AsociacionMultiplicidadMetadata
    {
        public const string NOMBRE = "AsociacionMultiplicidad";
        public const string NOMBRE_PLURAL = "AsociacionMultiplicidades";

        public const string ETIQUETA = "Multiplicidad de Asociación";
        public const string ETIQUETA_PLURAL = "Multiplicidades de Asociación";

        public class BD
        {
            public const string TABLA = "AsociacionMultiplicidades";
            public const string ID = "AsociacionMultiplicidadId";
        }

        public class Nombre
        {
            public const string ETIQUETA = "Nombre";
            public const int TAMAÑO_MAX = 50;
        }
    }
}
