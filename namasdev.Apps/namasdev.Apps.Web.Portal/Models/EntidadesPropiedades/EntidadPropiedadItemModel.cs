using System;
using System.ComponentModel.DataAnnotations;

using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.Models.EntidadesPropiedades
{
    public class EntidadPropiedadItemModel
    {
        public Guid Id { get; set; }
        public Guid EntidadId { get; set; }
        
        [Display(Name = EntidadPropiedadMetadata.Propiedades.Nombre.ETIQUETA)]
        public string Nombre { get; set; }
        
        [Display(Name = EntidadPropiedadMetadata.Propiedades.PropiedadTipoId.ETIQUETA)]
        public short PropiedadTipoId { get; set; }
        
        [Display(Name = EntidadPropiedadMetadata.Propiedades.PropiedadTipoId.ETIQUETA)]
        public string TipoNombre { get; set; }
        
        [Display(Name = EntidadPropiedadMetadata.Propiedades.PermiteNull.ETIQUETA)]
        public bool PermiteNull { get; set; }
        
        [Display(Name = EntidadPropiedadMetadata.Propiedades.Orden.ETIQUETA)]
        public short Orden { get; set; }

        public short OrdenInicial { get; set; }
        public bool Seleccionado { get; set; }

        public bool EsClave { get; set; }
        public bool EsID { get; set; }
        public bool EsAuditoria { get; set; }

        public bool OrdenModificado
        {
            get { return Orden != OrdenInicial; }
        }

        public bool EsIDoAuditoria
        {
            get { return EsID || EsAuditoria; }
        }

        public bool EditarDisponible
        {
            get { return EsID || !EsAuditoria; }
        }

        public bool OrdenarDisponible
        {
            get { return !EsIDoAuditoria; }
        }

        public bool EliminarDisponible
        {
            get { return !EsIDoAuditoria; }
        }
    }
}
