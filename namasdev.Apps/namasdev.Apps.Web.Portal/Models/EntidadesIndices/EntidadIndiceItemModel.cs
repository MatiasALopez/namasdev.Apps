
using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.EntidadesIndices
{
    public class EntidadIndiceItemModel
    {
        [Display(Name = EntidadIndiceMetadata.Propiedades.Id.ETIQUETA)]
        public Guid Id { get; set; }
        
        [Display(Name = EntidadIndiceMetadata.Propiedades.Nombre.ETIQUETA)]
        public string Nombre { get; set; }
    }
}
