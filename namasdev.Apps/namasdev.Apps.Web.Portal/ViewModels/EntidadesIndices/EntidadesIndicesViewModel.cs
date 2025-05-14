using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Web.ViewModels;

using namasdev.Apps.Web.Portal.Models.EntidadesIndices;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesIndices
{
    public class EntidadesIndicesViewModel : ListadoViewModel<EntidadIndiceItemModel>
    {
        public Guid Id { get; set; }
        public Guid AplicacionVersionId { get; set; }

        [Display(Name = EntidadMetadata.ETIQUETA)]
        public string EntidadNombre { get; set; }
    }
}
