using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Web.ViewModels;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Web.Portal.Models.AplicacionesVersiones;

namespace namasdev.Apps.Web.Portal.ViewModels.AplicacionesVersiones
{
    public class AplicacionesVersionesViewModel : ListadoConPaginacionViewModel<AplicacionVersionItemModel>
    {
        public AplicacionesVersionesViewModel()
        {
            ItemsPorPagina = 10;    
        }

        public Guid AplicacionId { get; set; }

        [Display(Name = AplicacionVersionMetadata.AplicacionId.DISPLAY_NAME)]
        public string AplicacionNombre { get; set; }

        [Display(Name = "Búsqueda (Nombre)")]
        public string Busqueda { get; set; }
    }
}