using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Web.ViewModels;

using namasdev.Apps.Web.Portal.Models.EntidadesAsociaciones;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesAsociaciones
{
    public class EntidadesAsociacionesViewModel : ListadoViewModel<EntidadAsociacionItemModel>
    {
        public Guid Id { get; set; }
        public Guid AplicacionVersionId { get; set; }

        [Display(Name = EntidadMetadata.ETIQUETA)]
        public string EntidadNombre { get; set; }
    }
}
