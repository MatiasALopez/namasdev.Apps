using namasdev.Core.Types;

namespace namasdev.Apps.Entidades
{
    public class PropiedadTipoEspecificacionesDecimal : PropiedadTipoEspecificacionesDecimalBase<decimal>
    {
        public decimal ValorMinimoDesdeDigitos
        {
            get { return -decimal.Parse(NumerosHelper.CrearNumeroDesdeDigitos(DigitosEnteros, DigitosDecimales)); }
        }

        public decimal ValorMaximoDesdeDigitos
        {
            get { return decimal.Parse(NumerosHelper.CrearNumeroDesdeDigitos(DigitosEnteros, DigitosDecimales)); }
        }
    }
}
