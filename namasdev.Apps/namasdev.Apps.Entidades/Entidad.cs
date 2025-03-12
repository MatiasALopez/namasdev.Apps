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
        public virtual EntidadPropiedadesDefault PropiedadesDefault { get; set; }
        public virtual List<EntidadPropiedad> Propiedades { get; set; }
        public virtual List<EntidadAsociacion> Asociaciones { get; set; }
        public virtual List<EntidadClave> Claves { get; set; }
        public virtual List<EntidadIndice> Indices { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
