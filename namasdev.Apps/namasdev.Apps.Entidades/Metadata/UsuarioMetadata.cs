﻿
namespace namasdev.Apps.Entidades.Metadata
{
    public class UsuarioMetadata
    {
        public const string NOMBRE = "Usuario";
        public const string NOMBRE_PLURAL = "Usuarios";

        public const string ETIQUETA = "Usuario";
        public const string ETIQUETA_PLURAL = "Usuarios";

        public class BD
        {
            public const string TABLA = "Usuarios";
            public const string TABLA_ID = "UsuarioId";
        }

        public class Propiedades
        {
            public class UsuarioId
            {
                public const int TAMAÑO_MAX = 128;
            }

            public class Email
            {
                public const string ETIQUETA = "Correo electrónico";
                public const int TAMAÑO_MAX = 100;
            }

            public class Nombres
            {
                public const string ETIQUETA = "Nombres";
                public const int TAMAÑO_MAX = 100;
            }

            public class Apellidos
            {
                public const string ETIQUETA = "Apellidos";
                public const int TAMAÑO_MAX = 100;
            }

            public class NombresYApellidos
            {
                public const string ETIQUETA = "Nombres y apellidos";
            }

            public class ApellidosYNombres
            {
                public const string ETIQUETA = "Apellidos y nombres";
            }
        }

        public class Mensajes
        {
            public const string AGREGAR_OK = ErrorMetadata.ETIQUETA + " agregado correctamente.";
            public const string AGREGAR_ERROR = "No se pudo agregar el " + ErrorMetadata.ETIQUETA;

            public const string EDITAR_OK = ErrorMetadata.ETIQUETA + " actualizado correctamente.";
            public const string EDITAR_ERROR = "No se pudo actualizar el " + ErrorMetadata.ETIQUETA;

            public const string ELIMINAR_OK = ErrorMetadata.ETIQUETA + " eliminado correctamente.";
            public const string ELIMINAR_ERROR = "No se pudo eliminar el " + ErrorMetadata.ETIQUETA;
        }
    }
}
