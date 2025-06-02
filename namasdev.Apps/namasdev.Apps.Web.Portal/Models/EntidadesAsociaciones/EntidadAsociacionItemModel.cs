using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.EntidadesAsociaciones
{
    public class EntidadAsociacionItemModel
    {
        [Display(Name = EntidadAsociacionMetadata.Propiedades.Id.ETIQUETA)]
        public Guid Id { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.Nombre.ETIQUETA)]
        public string Nombre { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.OrigenEntidadPropiedadNavegacionNombre.ETIQUETA)]
        public string OrigenEntidadPropiedadNavegacionNombre { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.DestinoEntidadPropiedadNavegacionNombre.ETIQUETA)]
        public string DestinoEntidadPropiedadNavegacionNombre { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.DeleteAsociacionReglaId.ETIQUETA)]
        public string DeleteReglaNombre { get; set; }

        [Display(Name = EntidadAsociacionMetadata.Propiedades.UpdateAsociacionReglaId.ETIQUETA)]
        public string UpdateReglaNombre { get; set; }
    }
}