using namasdev.Core.Types;

namespace namasdev.Apps.Entidades
{
    public class PropiedadTipoEspecificacionesDecimalFlotante : PropiedadTipoEspecificacionesDecimalBase<double>
    {
        public double ValorMinimoDesdeDigitos
        {
            get { return -double.Parse(NumerosHelper.CrearNumeroDesdeDigitos(DigitosEnteros, DigitosDecimales)); }
        }

        public double ValorMaximoDesdeDigitos
        {
            get { return double.Parse(NumerosHelper.CrearNumeroDesdeDigitos(DigitosEnteros, DigitosDecimales)); }
        }
    }
}
