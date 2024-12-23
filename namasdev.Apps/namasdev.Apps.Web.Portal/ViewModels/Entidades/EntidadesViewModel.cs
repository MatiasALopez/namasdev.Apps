using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using namasdev.Web.ViewModels;

using namasdev.Apps.Web.Portal.Models.Entidades;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Web.Portal.ViewModels.Entidades
{
    public class EntidadesViewModel : ListadoConPaginacionViewModel<EntidadItemModel>
    {
        public EntidadesViewModel()
        {
            ItemsPorPagina = 10;    
        }

        public Guid AplicacionId { get; set; }

        [Display(Name = AplicacionVersionMetadata.NOMBRE)]
        public Guid? AplicacionVersionId { get; set; }

        [Display(Name = AplicacionMetadata.NOMBRE)]
        public string AplicacionNombre { get; set; }
        public string AplicacionVersionNombre { get; set; }

        [Display(Name = "Búsqueda (Nombre)")]
        public string Busqueda { get; set; }

        public SelectList AplicacionesVersionesSelectList { get; set; }
    }
}