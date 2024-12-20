using System;
using System.Collections.Generic;
using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class Entidad : EntidadCreadoModificadoBorrado<Guid>
    {
        public Guid AplicacionVersionId { get; set; }
        public string Nombre { get; set; }

        public AplicacionVersion AplicacionVersion { get; set; }
        public List<EntidadPropiedad> Propiedades { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
