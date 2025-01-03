namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadPropiedadMetadata
    {
        public const string NOMBRE = "Propiedad";
        public const string NOMBRE_PLURAL = "Propiedades";

        public const string ETIQUETA = "Propiedad";
        public const string ETIQUETA_PLURAL = "Propiedades";

        public class BD
        {
            public const string TABLA = "EntidadesPropiedades";
            public const string ID = "EntidadPropiedadId";
        }

        public class EntidadId
        {
            public const string ETIQUETA = EntidadMetadata.ETIQUETA;
        }

        public class Nombre
        {
            public const string ETIQUETA = "Nombre";
            public const int TAMAÑO_MAX = 100;
            public const string REG_EX = @"^[a-zA-Z][a-zA-ZñÑ0-9_]*$";
        }

        public class Etiqueta
        {
            public const string ETIQUETA = "Etiqueta";
            public const int TAMAÑO_MAX = 100;
        }

        public class PropiedadTipoId
        {
            public const string ETIQUETA = PropiedadTipoMetadata.ETIQUETA;
        }

        public class PropiedadTipoEspecificaciones
        {
            public const string ETIQUETA = "Tipo Especificaciones";
        }

        public class Orden
        {
            public const string ETIQUETA = "Orden";
        }

        public class PermiteNull
        {
            public const string ETIQUETA = "Permite null";
        }

        public class GeneradaAlCrear
        {
            public const string ETIQUETA = "Generada al crear";
        }

        public class Editable
        {
            public const string ETIQUETA = "Editable";
        }

        public class CalculadaFormula
        {
            public const string ETIQUETA = "Calculada (Fórmula)";
            public const int TAMAÑO_MAX = 2000;
        }
    }
}
