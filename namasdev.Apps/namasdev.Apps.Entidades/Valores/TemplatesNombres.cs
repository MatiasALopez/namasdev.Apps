namespace namasdev.Apps.Entidades.Valores
{
    public class TemplatesNombres
    {
        public class BD
        {
            public const string Tabla = "BD_Tabla";
        }

        public class Entidades
        {
            public const string ENTIDAD = "Entidades_Entidad";

            public class Metadata
            {
                public const string ENTIDAD_METADATA = "Entidades_Metadata_EntidadMetadata";
            }
        }

        public class Datos
        {
            public const string REPOSITORIO = "Datos_Repositorio";

            public class Sql
            {
                public const string CONFIG = "Datos_Sql_Config";
            }
        }
        
        public class Negocio
        {
            public const string NEGOCIO = "Negocio_Negocio";

            public class Automapper
            {
                public const string PROFILE = "Negocio_Automapper_Profile";
            }

            public class DTO
            {
                public const string AGREGAR_PARAMETROS = "Negocio_DTO_AgregarParametros";
                public const string ACTUALIZAR_PARAMETROS = "Negocio_DTO_ActualizarParametros";
                public const string MARCAR_COMO_BORRADO_PARAMETROS = "Negocio_DTO_MarcarComoBorradoParametros";
                public const string DESMARCAR_COMO_BORRADO_PARAMETROS = "Negocio_DTO_DesmarcarComoBorradoParametros";
                public const string ELIMINAR_PARAMETROS = "Negocio_DTO_EliminarParametros";
            }
        }

        public class Web
        {
            public const string CONTROLLER = "Web_Controller";

            public class Models
            {
                public const string ITEM_MODEL = "Web_Models_ItemModel";
            }

            public class ViewModels
            {
                public const string ENTIDAD_VIEW_MODEL = "Web_ViewModels_EntidadViewModel";
                public const string LISTA_VIEW_MODEL = "Web_ViewModels_ListaViewModel";
            }

            public class Automapper
            {
                public const string PROFILE = "Web_Automapper_Profile";
            }

            public class Metadata
            {
                public const string VIEWS = "Web_Metadata_Views";
            }

            public class Views
            {
                public const string INDEX = "Web_Views_Index";
                public const string ENTIDAD = "Web_Views_Entidad";
            }
        }
    }
}
