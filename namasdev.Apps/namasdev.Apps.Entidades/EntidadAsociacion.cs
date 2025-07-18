﻿using System;

using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public class EntidadAsociacion : Entidad<Guid>
    {
        public Guid OrigenEntidadId { get; set; }
        public string Nombre { get; set; }
        public Guid OrigenEntidadPropiedadId { get; set; }
        public string OrigenEntidadPropiedadNavegacionNombre { get; set; }
        public short OrigenAsociacionMultiplicidadId { get; set; }
        public Guid DestinoEntidadId { get; set; }
        public Guid DestinoEntidadPropiedadId { get; set; }
        public string DestinoEntidadPropiedadNavegacionNombre { get; set; }
        public short DestinoAsociacionMultiplicidadId { get; set; }
        public string TablaAuxiliarNombre { get; set; }
        public short DeleteAsociacionReglaId { get; set; }
        public short UpdateAsociacionReglaId { get; set; }

        public virtual Entidad OrigenEntidad { get; set; }
        public virtual EntidadPropiedad OrigenPropiedad { get; set; }
        public virtual AsociacionMultiplicidad OrigenMultiplicidad { get; set; }
        public virtual Entidad DestinoEntidad { get; set; }
        public virtual EntidadPropiedad DestinoPropiedad { get; set; }
        public virtual AsociacionMultiplicidad DestinoMultiplicidad { get; set; }
        public virtual AsociacionRegla DeleteRegla { get; set; }
        public virtual AsociacionRegla UpdateRegla { get; set; }
    }
}
