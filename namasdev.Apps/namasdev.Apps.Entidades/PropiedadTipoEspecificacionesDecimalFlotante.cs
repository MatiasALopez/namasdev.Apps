namespace namasdev.Apps.Entidades
{
    public class PropiedadTipoEspecificacionesDecimalFlotante : PropiedadTipoEspecificacionesDecimalBase<double>
    {
        public double ValorMinimoDesdeDigitos
        {
            get { return -double.Parse(CrearNumeroDesdeDigitosString()); }
        }

        public double ValorMaximoDesdeDigitos
        {
            get { return double.Parse(CrearNumeroDesdeDigitosString()); }
        }
    }
}
