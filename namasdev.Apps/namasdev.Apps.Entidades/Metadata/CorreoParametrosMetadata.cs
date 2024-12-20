
namespace namasdev.Apps.Entidades.Metadata
{
    public class CorreoParametrosMetadata
    {
        public const string NOMBRE = "Parámetros correo";

        public class BD
        {
            public const string TABLA = "CorreosParametros";
            public const string ID = "Id";
        }

        public class Asunto
        {
            public const string DISPLAY_NAME = "Asunto";
            public const int TAMAÑO_MAX = 256;
        }

        public class Contenido
        {
            public const string DISPLAY_NAME = "Contenido";
        }

        public class CopiaOculta
        {
            public const string DISPLAY_NAME = "Copia oculta";
        }
    }
}
