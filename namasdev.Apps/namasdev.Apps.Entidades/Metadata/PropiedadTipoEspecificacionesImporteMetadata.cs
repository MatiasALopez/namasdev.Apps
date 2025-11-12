namespace namasdev.Apps.Entidades.Metadata
{
    public class PropiedadTipoEspecificacionesImporteMetadata
    {
        public const string NOMBRE = "EspecificacionesImporte";
        public const string ETIQUETA = "Especificaciones (Importe)";

        public class Propiedades
        {
            public class ValorMinimo
            {
                public const string ETIQUETA = "Valor mínimo";
                public const int TAMAÑO_MAX = 20;
            }

            public class ValorMaximo
            {
                public const string ETIQUETA = "Valor máximo";
                public const int TAMAÑO_MAX = 20;
            }
        }
    }
}
