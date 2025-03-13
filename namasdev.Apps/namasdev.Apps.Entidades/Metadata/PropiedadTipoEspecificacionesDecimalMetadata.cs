namespace namasdev.Apps.Entidades.Metadata
{
    public class PropiedadTipoEspecificacionesDecimalMetadata
    {
        public const string NOMBRE = "EspecificacionesDecimal";
        public const string ETIQUETA = "Especificaciones (Decimal)";

        public class ValorMinimo
        {
            public const string ETIQUETA = "Valor mínimo";
            public const int TAMAÑO_MAX = 50;
        }

        public class ValorMaximo
        {
            public const string ETIQUETA = "Valor máximo";
            public const int TAMAÑO_MAX = 50;
        }

        public class DigitosEnteros
        {
            public const string ETIQUETA = "Dígitos enteros";
            public const int TAMAÑO_MAX = 3;
        }

        public class DigitosDecimales
        {
            public const string ETIQUETA = "Dígitos decimales";
            public const int TAMAÑO_MAX = 3;
        }
    }
}
