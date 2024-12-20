namespace namasdev.Apps.Entidades.Metadata
{
    public class ErrorMetadata
    {
        public const string NOMBRE = "Error";

        public class BD
        {
            public const string TABLA = "Errores";
            public const string ID = "Id";
        }

        public class Mensaje
        {
            public const string DISPLAY_NAME = "Mensaje";
        }

        public class StackTrace
        {
            public const string DISPLAY_NAME = "Stack Trace";
        }

        public class Source
        {
            public const string DISPLAY_NAME = "Source";
            public const int TAMAÑO_MAX = 200;
        }

        public class Argumentos
        {
            public const string DISPLAY_NAME = "Argumentos";
        }

        public class FechaHora
        {
            public const string DISPLAY_NAME = "Fecha/Hora";
        }

        public class UserId
        {
            public const string DISPLAY_NAME = "User";
            public const int TAMAÑO_MAX = 128;
        }
    }
}
