using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

using namasdev.Web.ViewModels;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class EntidadesPropiedadesViewModel : ListadoViewModel<EntidadPropiedadItemModel>
    {
        public const string OPERACION_ESTABLECER_CLAVE = "EstablecerClave";

        public Guid EntidadId { get; set; }
        public Guid AplicacionVersionId { get; set; }

        [Display(Name = EntidadMetadata.ETIQUETA)]
        public string EntidadNombre { get; set; }

        [Display(Name = "Búsqueda (Nombre)")]
        public string Busqueda { get; set; }

        public string Operacion { get; set; }

        public IEnumerable<EntidadPropiedadItemModel> ItemsSeleccionados
        {
            get 
            {
                return Items?
                    .Where(i => i.Seleccionado)
                    .ToArray();
            }
        }
    }
}