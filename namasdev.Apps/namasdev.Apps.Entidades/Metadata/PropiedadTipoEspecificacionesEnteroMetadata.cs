namespace namasdev.Apps.Entidades.Metadata
{
    public class PropiedadTipoEspecificacionesEnteroMetadata
    {
        public const string NOMBRE = "EspecificacionesEntero";
        public const string ETIQUETA = "Especificaciones (Entero)";

        public class Propiedades
        {
            public class ValorMinimo
            {
                public const string ETIQUETA = "Valor mínimo";
                public const int TAMAÑO_MAX = 10;
            }

            public class ValorMaximo
            {
                public const string ETIQUETA = "Valor máximo";
                public const int TAMAÑO_MAX = 10;
            }
        }
    }
}
