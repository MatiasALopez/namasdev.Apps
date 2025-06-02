namespace namasdev.Apps.Negocio.DTO.EntidadesEspecificaciones
{
    public class AgregarParametros : ParametrosConUsuarioBase
    {
        public short ArticuloId { get; set; }
        public short? IDPropiedadTipoId { get; set; }
        public bool EsSoloLectura { get; set; }
        public short BajaTipoId { get; set; }
        public bool AuditoriaCreado { get; set; }
        public bool AuditoriaUltimaModificacion { get; set; }
    }
}
