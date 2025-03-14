using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.EntidadesAsociaciones
{
    public class EntidadAsociacionItemModel
    {
        public Guid EntidadAsociacionId { get; set; }
        public Guid EntidadId { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.OrigenEntidadPropiedadId.ETIQUETA)]
        public string OrigenPropiedadNombre { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.OrigenEntidadPropiedadId.ETIQUETA)]
        public string OrigenMultiplicidadNombre { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.DestinoEntidadPropiedadId.ETIQUETA)]
        public string DestinoPropiedadNombre { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.DestinoAsociacionMultiplicidadId.ETIQUETA)]
        public string DestinoMultiplicidadNombre { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.DeleteAsociacionReglaId.ETIQUETA)]
        public string DeleteReglaNombre { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.UpdateAsociacionReglaId.ETIQUETA)]
        public string UpdateReglaNombre { get; set; }
    }
}