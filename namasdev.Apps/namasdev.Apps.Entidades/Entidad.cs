using System;
using System.Collections.Generic;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class Entidad : EntidadCreadoModificadoBorrado<Guid>
    {
        public Guid AplicacionVersionId { get; set; }
        public string Nombre { get; set; }
        public string NombrePlural { get; set; }
        public string Etiqueta { get; set; }
        public string EtiquetaPlural { get; set; }

        public virtual AplicacionVersion AplicacionVersion { get; set; }
        public virtual EntidadEspecificaciones Especificaciones { get; set; }
        public virtual List<EntidadPropiedad> Propiedades { get; set; }
        public virtual List<EntidadAsociacion> AsociacionesOrigen { get; set; }
        public virtual List<EntidadAsociacion> AsociacionesDestino { get; set; }
        public virtual List<EntidadClave> Claves { get; set; }
        public virtual List<EntidadIndice> Indices { get; set; }
        public virtual List<EntidadCheck> Checks { get; set; }

        public bool TienePropiedad(string nombre)
        {
            return Propiedades?.Exists(p => string.Equals(p.Nombre, nombre, StringComparison.OrdinalIgnoreCase)) ?? false;
        }

        public EntidadPropiedad ObtenerPropiedad(string nombre)
        {
            return Propiedades?.Find(p => string.Equals(p.Nombre, nombre, StringComparison.OrdinalIgnoreCase));
        }

        public bool TienePropiedadID()
        {
            return Propiedades?.Exists(p => p.EsID) ?? false;
        }

        public EntidadPropiedad ObtenerPropiedadID()
        {
            return Propiedades?.Find(p => p.EsID);
        }

        public bool TieneClave(Guid entidadPropiedadId)
        {
            return Claves?.Exists(c => c.EntidadPropiedadId == entidadPropiedadId) ?? false;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
