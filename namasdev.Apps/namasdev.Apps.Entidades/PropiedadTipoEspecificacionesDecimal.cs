namespace namasdev.Apps.Entidades
{
    public class PropiedadTipoEspecificacionesDecimal : PropiedadTipoEspecificacionesDecimalBase<decimal>
    {
        public decimal ValorMinimoDesdeDigitos
        {
            get { return -decimal.Parse(CrearNumeroDesdeDigitosString()); }
        }

        public decimal ValorMaximoDesdeDigitos
        {
            get { return decimal.Parse(CrearNumeroDesdeDigitosString()); }
        }
    }
}
