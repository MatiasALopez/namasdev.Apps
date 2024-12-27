namespace namasdev.Apps.Entidades
{
    public class PropiedadTipoEspecificacionesNumeroBase<T> : IPropiedadTipoEspecificaciones
        where T : struct
    {
        public T? ValorMinimo { get; set; }
        public T? ValorMaximo { get; set; }
        public int CantDecimales { get; set; }
    }
}
