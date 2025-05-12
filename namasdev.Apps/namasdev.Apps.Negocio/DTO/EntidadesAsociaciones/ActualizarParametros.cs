
using System;

namespace namasdev.Apps.Negocio.DTO.EntidadesAsociaciones
{
    public class ActualizarParametros : ParametrosEntidadBase<Guid>
    {
        public string Nombre { get; set; }
        public Guid OrigenEntidadId { get; set; }
        public Guid OrigenEntidadPropiedadId { get; set; }
        public short OrigenAsociacionMultiplicidadId { get; set; }
        public Guid DestinoEntidadId { get; set; }
        public Guid DestinoEntidadPropiedadId { get; set; }
        public short DestinoAsociacionMultiplicidadId { get; set; }
        public string TablaAuxiliarNombre { get; set; }
        public short DeleteAsociacionReglaId { get; set; }
        public short UpdateAsociacionReglaId { get; set; }
    }
}
