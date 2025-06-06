using namasdev.Apps.Entidades.Valores;

namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadPropiedadMetadata
    {
        public const string NOMBRE = "EntidadPropiedad";
        public const string NOMBRE_PLURAL = "EntidadesPropiedades";

        public const string ETIQUETA = "Propiedad";
        public const string ETIQUETA_PLURAL = "Propiedades";

        public class BD
        {
            public const string TABLA = "EntidadesPropiedades";
            public const string ID = "EntidadPropiedadId";
        }

        public class Propiedades
        {
            public class Id
            {
                public const string ETIQUETA = "ID";
            }
            public class EntidadId
            {
                public const string ETIQUETA = EntidadMetadata.ETIQUETA;
            }
            public class Nombre
            {
                public const string ETIQUETA = "Nombre";
                public const int TAMAÑO_MAX = 100;
                public const string REG_EX = RegExs.IDENTIFICADOR;
            }
            public class Etiqueta
            {
                public const string ETIQUETA = "Etiqueta";
                public const int TAMAÑO_MAX = 100;
            }
            public class PropiedadTipoId
            {
                public const string ETIQUETA = PropiedadTipoMetadata.ETIQUETA;
            }
            public class PropiedadTipoEspecificaciones
            {
                public const string ETIQUETA = "Tipo especificaciones";
            }
            public class PermiteNull
            {
                public const string ETIQUETA = "Permite NULL";
            }
            public class Orden
            {
                public const string ETIQUETA = "Orden";
            }
            public class CalculadaFormula
            {
                public const string ETIQUETA = "Calculada (Fórmula)";
            }
            public class GeneradaAlCrear
            {
                public const string ETIQUETA = "Generada al crear";
            }
            public class Editable
            {
                public const string ETIQUETA = "Editable";
            }
            public class EsID
            {
                public const string ETIQUETA = "Es ID";
            }
            public class EsAuditoria
            {
                public const string ETIQUETA = "Es Auditoría";
            }
        }

        public class Mensajes
        {
            public const string AGREGAR_OK = EntidadPropiedadMetadata.ETIQUETA + " agregada correctamente.";
            public const string AGREGAR_ERROR = "No se pudo agregar la " + EntidadPropiedadMetadata.ETIQUETA;

            public const string EDITAR_OK = EntidadPropiedadMetadata.ETIQUETA + " actualizada correctamente.";
            public const string EDITAR_ERROR = "No se pudo actualizar la " + EntidadPropiedadMetadata.ETIQUETA;

            public const string ELIMINAR_OK = EntidadPropiedadMetadata.ETIQUETA + " eliminada correctamente.";
            public const string ELIMINAR_ERROR = "No se pudo eliminar la " + EntidadPropiedadMetadata.ETIQUETA;
            public const string ELIMINAR_CONFIRMACION = "¿Estás seguro que deseas eliminar la " + EntidadPropiedadMetadata.ETIQUETA + " seleccionada?";

            public const string ESTABLECER_CLAVE_OK = "Clave establecida correctamente.";
            public const string ESTABLECER_CLAVE_ERROR = "No se pudo establecer la clave.";

            public const string ACTUALIZAR_ORDEN_OK = "Orden actualizado correctamente.";
            public const string ACTUALIZAR_ORDEN_ERROR = "No se pudo actualizar el orden.";
        }
    }
}
