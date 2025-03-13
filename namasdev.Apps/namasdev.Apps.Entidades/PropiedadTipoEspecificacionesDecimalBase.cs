namespace namasdev.Apps.Entidades
{
    public abstract class PropiedadTipoEspecificacionesDecimalBase<T> : IPropiedadTipoEspecificaciones
        where T : struct
    {
        public T? ValorMinimo { get; set; }
        public T? ValorMaximo { get; set; }
        public short DigitosEnteros { get; set; }
        public short DigitosDecimales { get; set; }
    }
}
