using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Web.ViewModels;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class EntidadesPropiedadesViewModel : ListadoConPaginacionViewModel<EntidadPropiedadItemModel>
    {
        public EntidadesPropiedadesViewModel()
        {
            ItemsPorPagina = 10;    
        }

        public Guid EntidadId { get; set; }

        [Display(Name = EntidadMetadata.NOMBRE)]
        public string EntidadNombre { get; set; }

        [Display(Name = "Búsqueda (Nombre)")]
        public string Busqueda { get; set; }
    }
}