using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

using namasdev.Web.ViewModels;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Valores;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesPropiedades
{
    public class EntidadesPropiedadesViewModel : ListadoViewModel<EntidadPropiedadItemModel>
    {
        public const string OPERACION_ESTABLECER_CLAVE = "EstablecerClave";

        public Guid Id { get; set; }
        public Guid AplicacionVersionId { get; set; }

        [Display(Name = EntidadMetadata.ETIQUETA)]
        public string EntidadNombre { get; set; }

        [Display(Name = "Búsqueda (Nombre)")]
        public string Busqueda { get; set; }

        public string Operacion { get; set; }

        public IEnumerable<EntidadPropiedadItemModel> ItemsSeleccionados
        {
            get 
            {
                return Items?
                    .Where(i => i.Seleccionado)
                    .ToArray();
            }
        }

        public void EstablecerPropiedadesSoloLecturaSegunEspecificaciones(EntidadEspecificaciones especificaciones)
        {
            if (especificaciones == null)
            {
                return;
            }

            foreach (var i in Items)
            {
                i.DefinidoPorEspecificaciones =
                    (especificaciones.IDPropiedadTipoId.HasValue && i.EsId)
                    || (especificaciones.AuditoriaCreado && i.EsAuditoriaCreado)
                    || (especificaciones.AuditoriaUltimaModificacion && i.EsAuditoriaUltimaModificacion)
                    || (especificaciones.BajaTipoId == BajaTipos.LOGICA && i.EsAuditoriaBorrado);
            }
        }
    }
}