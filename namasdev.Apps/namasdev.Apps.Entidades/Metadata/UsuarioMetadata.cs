
namespace namasdev.Apps.Entidades.Metadata
{
    public class UsuarioMetadata
    {
        public const string NOMBRE = "Usuario";

        public class BD
        {
            public const string TABLA = "Usuarios";
            public const string TABLA_ID = "UsuarioId";
        }

        public class UsuarioId
        {
            public const int TAMAÑO_MAX = 128;
        }

        public class Email
        {
            public const string DISPLAY_NAME = "Correo electrónico";
            public const int TAMAÑO_MAX = 100;
        }

        public class Nombres
        {
            public const string DISPLAY_NAME = "Nombres";
            public const int TAMAÑO_MAX = 100;
        }

        public class Apellidos
        {
            public const string DISPLAY_NAME = "Apellidos";
            public const int TAMAÑO_MAX = 100;
        }

        public class NombresYApellidos
        {
            public const string DISPLAY_NAME = "Nombres y apellidos";
        }

        public class ApellidosYNombres
        {
            public const string DISPLAY_NAME = "Apellidos y nombres";
        }
    }
}
