namespace namasdev.Apps.Entidades
{
    public class PropiedadTipoEspecificacionesEnteroBase<T> : IPropiedadTipoEspecificaciones
        where T : struct
    {
        public T? ValorMinimo { get; set; }
        public T? ValorMaximo { get; set; }
    }
}
