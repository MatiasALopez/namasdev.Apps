using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.EntidadesChecks
{
    public class EntidadCheckItemModel
    {
        [Display(Name = EntidadCheckMetadata.Propiedades.Id.ETIQUETA)]
        public Guid Id { get; set; }
        
        [Display(Name = EntidadCheckMetadata.Propiedades.Nombre.ETIQUETA)]
        public string Nombre { get; set; }
    }
}
