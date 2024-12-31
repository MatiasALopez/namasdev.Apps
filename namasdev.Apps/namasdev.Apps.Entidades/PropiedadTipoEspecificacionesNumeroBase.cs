namespace namasdev.Apps.Entidades
{
    public abstract class PropiedadTipoEspecificacionesNumeroBase<T> : IPropiedadTipoEspecificaciones
        where T : struct
    {
        public T? ValorMinimo { get; set; }
        public T? ValorMaximo { get; set; }
        public int CantDecimales { get; set; }
    }
}
