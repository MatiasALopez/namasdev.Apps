using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Web.ViewModels;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Web.Portal.Models.Versiones;

namespace namasdev.Apps.Web.Portal.ViewModels.Versiones
{
    public class VersionesViewModel : ListadoConPaginacionViewModel<VersionItemModel>
    {
        public VersionesViewModel()
        {
            ItemsPorPagina = 10;    
        }

        public Guid AplicacionId { get; set; }

        [Display(Name = AplicacionVersionMetadata.AplicacionId.ETIQUETA)]
        public string AplicacionNombre { get; set; }

        [Display(Name = "Búsqueda (Nombre)")]
        public string Busqueda { get; set; }
    }
}