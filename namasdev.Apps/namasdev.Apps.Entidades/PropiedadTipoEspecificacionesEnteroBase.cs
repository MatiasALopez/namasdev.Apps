namespace namasdev.Apps.Entidades
{
    public abstract class PropiedadTipoEspecificacionesEnteroBase<T> : IPropiedadTipoEspecificaciones
        where T : struct
    {
        public T? ValorMinimo { get; set; }
        public T? ValorMaximo { get; set; }
    }
}
