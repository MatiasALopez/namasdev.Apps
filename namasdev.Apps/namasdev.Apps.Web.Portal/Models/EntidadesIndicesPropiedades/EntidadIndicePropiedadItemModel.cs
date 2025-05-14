
using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.EntidadesIndicesPropiedades
{
    public class EntidadIndicePropiedadItemModel
    {
        [Display(Name = EntidadIndicePropiedadMetadata.Propiedades.Id.ETIQUETA)]
        public Guid Id { get; set; }
        
        [Display(Name = EntidadIndicePropiedadMetadata.Propiedades.EntidadIndiceId.ETIQUETA)]
        public Guid EntidadIndiceId { get; set; }
        
        [Display(Name = EntidadIndicePropiedadMetadata.Propiedades.EntidadPropiedadId.ETIQUETA)]
        public Guid EntidadPropiedadId { get; set; }
        
    }
}
