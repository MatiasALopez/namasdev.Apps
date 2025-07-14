using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Web.ViewModels;

using namasdev.Apps.Web.Portal.Models.EntidadesChecks;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesChecks
{
    public class EntidadesChecksViewModel : ListadoViewModel<EntidadCheckItemModel>
    {
        public Guid Id { get; set; }
        public Guid AplicacionVersionId { get; set; }

        [Display(Name = EntidadMetadata.ETIQUETA)]
        public string EntidadNombre { get; set; }
    }
}
