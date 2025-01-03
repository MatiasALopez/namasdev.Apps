namespace namasdev.Apps.Entidades
{
    public class PropiedadTipoEspecificacionesTexto : IPropiedadTipoEspecificaciones
    {
        public short? TamañoMinimo { get; set; }
        public short? TamañoMaximo { get; set; }
        public short? TamañoExacto { get; set; }
        public string RegEx { get; set; }
        public bool EsMultilinea { get; set; }
    }
}
