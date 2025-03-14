using System.ComponentModel.DataAnnotations;

using namasdev.Web.ViewModels;
using namasdev.Apps.Web.Portal.Models.Aplicaciones;

namespace namasdev.Apps.Web.Portal.ViewModels.Aplicaciones
{
    public class AplicacionesViewModel : ListadoConPaginacionViewModel<AplicacionItemModel>
    {
        public AplicacionesViewModel()
        {
            ItemsPorPagina = 20;    
        }

        [Display(Name = "Búsqueda (Nombre)")]
        public string Busqueda { get; set; }
    }
}