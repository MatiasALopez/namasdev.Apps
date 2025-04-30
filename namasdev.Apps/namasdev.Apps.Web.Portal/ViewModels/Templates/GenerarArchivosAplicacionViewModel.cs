using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using namasdev.Apps.Entidades.Metadata;
using namasdev.Core.Validation;

namespace namasdev.Apps.Web.Portal.ViewModels.Templates
{
    public class GenerarArchivosAplicacionViewModel : GenerarArchivosViewModelBase
    {
        public Guid Id { get; set; }

        [Display(Name = AplicacionVersionMetadata.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public Guid? AplicacionVersionId { get; set; }

        public SelectList VersionesSelectList { get; set; }
    }
}