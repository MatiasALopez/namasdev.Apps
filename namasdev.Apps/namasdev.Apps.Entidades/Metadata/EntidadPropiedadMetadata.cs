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
            public const string DISPLAY_NAME = EntidadMetadata.NOMBRE;
        }

        public class Nombre
        {
            public const string DISPLAY_NAME = "Nombre";
            public const int TAMAÑO_MAX = 100;
            public const string REG_EX = @"^[a-zA-Z][a-zA-ZñÑ0-9_]*$";
        }

        public class Etiqueta
        {
            public const string DISPLAY_NAME = "Etiqueta";
            public const int TAMAÑO_MAX = 100;
        }

        public class PropiedadTipoId
        {
            public const string DISPLAY_NAME = PropiedadTipoMetadata.NOMBRE;
        }

        public class PropiedadTipoEspecificaciones
        {
            public const string DISPLAY_NAME = "Tipo Especificaciones";
        }

        public class Orden
        {
            public const string DISPLAY_NAME = "Orden";
        }

        public class PermiteNull
        {
            public const string DISPLAY_NAME = "Permite null";
        }

        public class GeneradaAlCrear
        {
            public const string DISPLAY_NAME = "Generada al crear";
        }

        public class Editable
        {
            public const string DISPLAY_NAME = "Editable";
        }

        public class CalculadaFormula
        {
            public const string DISPLAY_NAME = "Calculada (Fórmula)";
            public const int TAMAÑO_MAX = 2000;
        }
    }
}
