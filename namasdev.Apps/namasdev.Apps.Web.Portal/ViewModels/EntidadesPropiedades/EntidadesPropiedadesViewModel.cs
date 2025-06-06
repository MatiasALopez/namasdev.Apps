using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

using namasdev.Web.ViewModels;

using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class EntidadesPropiedadesViewModel : ListadoViewModel<EntidadPropiedadItemModel>
    {
        public const string OPERACION_ESTABLECER_CLAVE = "EstablecerClave";
        public const string OPERACION_ACTUALIZAR_ORDEN = "ActualizarOrden";

        public Guid Id { get; set; }
        public Guid AplicacionVersionId { get; set; }

        [Display(Name = EntidadMetadata.ETIQUETA)]
        public string EntidadNombre { get; set; }

        public string Operacion { get; set; }

        public IEnumerable<Guid> ItemsSeleccionadosIds
        {
            get
            {
                return Items?
                    .Where(i => i.Seleccionado)
                    .Select(i => i.Id)
                    .ToArray();
            }
        }

        public Dictionary<Guid, short> ItemsConOrdenesModificados
        {
            get
            {
                return Items?
                    .Where(i => i.OrdenModificado)
                    .ToDictionary(i => i.Id, i => i.Orden);
            }
        }
    }
}
