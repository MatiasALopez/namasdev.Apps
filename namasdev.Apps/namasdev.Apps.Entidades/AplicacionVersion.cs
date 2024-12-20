using System;
using System.Collections.Generic;
using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class AplicacionVersion : EntidadCreadoModificadoBorrado<Guid>
    {
        public Guid AplicacionId { get; set; }
        public string Nombre { get; set; }

        public Aplicacion Aplicacion { get; set; }
        public List<Entidad> Entidades { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
