
namespace namasdev.Apps.Entidades.Metadata
{
    public class ParametroMetadata
    {
        public const string NOMBRE = "Aplicación";

        public class BD
        {
            public const string TABLA = "Parametros";
            public const string ID = "Nombre";
        }

        public class Nombre
        {
            public const string DISPLAY_NAME = "Nombre";
            public const int TAMAÑO_MAX = 100;
        }

        public class Valor
        {
            public const string DISPLAY_NAME = "Valor";
        }

        public class Nombres
        {
            #region Generales

            public const string SITIO_URL = "SitioUrl";
            public const string ERRORES_MENSAJE_DEFAULT = "ErroresMensajeDefault";

            #endregion

            #region Correos

            public const string CORREOS_REMITENTE = "CorreosRemitente";

            #endregion

            #region Servidor Correos

            public const string SERVIDOR_CORREOS = "ServidorCorreos";
            public const string SERVIDOR_CORREOS_HOST = "ServidorCorreosHost";
            public const string SERVIDOR_CORREOS_PUERTO = "ServidorCorreosPuerto";
            public const string SERVIDOR_CORREOS_CREDENCIALES_USUARIO = "ServidorCorreosCredencialesUsuario";
            public const string SERVIDOR_CORREOS_CREDENCIALES_PASSWORD = "ServidorCorreosCredencialesPassword";
            public const string SERVIDOR_CORREOS_HABILITAR_SSL = "ServidorCorreosHabilitarSSL";
            public const string SERVIDOR_CORREOS_COPIA_OCULTA = "ServidorCorreosCopiaOculta";
            public const string SERVIDOR_CORREOS_HEADERS = "ServidorCorreosHeaders";

            #endregion

            #region Azure Storage

            public const string CLOUD_STORAGE_ACCOUNT_CONNECTION_STRING = "CloudStorageAccountConnectionString";

            #endregion
        }
    }
}
