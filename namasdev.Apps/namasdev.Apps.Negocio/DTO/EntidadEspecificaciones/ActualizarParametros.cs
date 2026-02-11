using System;

namespace namasdev.Apps.Negocio.DTO.EntidadesEspecificaciones
{
    public class ActualizarParametros : ParametrosEntidadBase<Guid>
    {
        public string IdiomaArticuloId { get; set; }
        public short? IDPropiedadTipoId { get; set; }
        public bool EsSoloLectura { get; set; }
        public short BajaTipoId { get; set; }
        public bool AuditoriaCreado { get; set; }
        public bool AuditoriaUltimaModificacion { get; set; }
    }
}
