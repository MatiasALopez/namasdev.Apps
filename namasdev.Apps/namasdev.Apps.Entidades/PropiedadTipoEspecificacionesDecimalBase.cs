namespace namasdev.Apps.Entidades
{
    public abstract class PropiedadTipoEspecificacionesDecimalBase<T> : IPropiedadTipoEspecificaciones
        where T : struct
    {
        public T? ValorMinimo { get; set; }
        public T? ValorMaximo { get; set; }
        public short DigitosEnteros { get; set; }
        public short DigitosDecimales { get; set; }

        protected string CrearNumeroDesdeDigitosString()
        {
            return $"{new string('9', DigitosEnteros)}.{new string('9', DigitosDecimales)}";
        }
    }
}
