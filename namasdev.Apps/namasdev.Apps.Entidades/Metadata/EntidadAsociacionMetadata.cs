﻿namespace namasdev.Apps.Entidades.Metadata
{
    public class EntidadAsociacionMetadata
    {
        public const string NOMBRE = "EntidadAsociacion";
        public const string NOMBRE_PLURAL = "EntidadesAsociaciones";

        public const string ETIQUETA = "Asociación";
        public const string ETIQUETA_PLURAL = "Asociaciones";

        public class BD
        {
            public const string TABLA = "EntidadesAsociaciones";
            public const string ID = "EntidadAsociacionId";
        }

        public class Propiedades
        {
            public class OrigenEntidadId
            {
                public const string ETIQUETA = "Entidad (origen)";
            }

            public class OrigenEntidadPropiedadId
            {
                public const string ETIQUETA = "Propiedad (origen)";
            }

            public class OrigenAsociacionMultiplicidadId
            {
                public const string ETIQUETA = "Multiplicidad (origen)";
            }

            public class DestinoEntidadPropiedadId
            {
                public const string ETIQUETA = "Propiedad (destino)";
            }

            public class DestinoAsociacionMultiplicidadId
            {
                public const string ETIQUETA = "Multiplicidad (destino)";
            }

            public class TablaAuxiliarNombre
            {
                public const string ETIQUETA = "Tabla auxiliar";
                public const int TAMAÑO_MAX = 100;
                public const string REG_EX = @"^[a-zA-ZñÑ0-9_]*$";
            }

            public class DeleteAsociacionReglaId
            {
                public const string ETIQUETA = "Regla DELETE";
            }

            public class UpdateAsociacionReglaId
            {
                public const string ETIQUETA = "Regla UPDATE";
            }
        }

        public class Mensajes
        {
            public const string AGREGAR_OK = EntidadAsociacionMetadata.ETIQUETA + " agregada correctamente.";
            public const string AGREGAR_ERROR = "No se pudo agregar la " + EntidadAsociacionMetadata.ETIQUETA;

            public const string EDITAR_OK = EntidadAsociacionMetadata.ETIQUETA + " actualizada correctamente.";
            public const string EDITAR_ERROR = "No se pudo actualizar la " + EntidadAsociacionMetadata.ETIQUETA;

            public const string ELIMINAR_OK = EntidadAsociacionMetadata.ETIQUETA + " eliminada correctamente.";
            public const string ELIMINAR_ERROR = "No se pudo eliminar la " + EntidadAsociacionMetadata.ETIQUETA;
        }
    }
}
