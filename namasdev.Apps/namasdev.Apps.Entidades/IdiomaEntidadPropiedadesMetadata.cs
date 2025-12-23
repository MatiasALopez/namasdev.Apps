using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class IdiomaEntidadPropiedadesMetadata : Entidad<string>
    {
        public string CreadoPorNombre { get; set; }
        public string CreadoPorEtiqueta { get; set; }
        public string CreadoFechaNombre { get; set; }
        public string CreadoFechaEtiqueta { get; set; }
        public string UltimaModificacionPorNombre { get; set; }
        public string UltimaModificacionPorEtiqueta { get; set; }
        public string UltimaModificacionFechaNombre { get; set; }
        public string UltimaModificacionFechaEtiqueta { get; set; }
        public string BorradoPorNombre { get; set; }
        public string BorradoPorEtiqueta { get; set; }
        public string BorradoFechaNombre { get; set; }
        public string BorradoFechaEtiqueta { get; set; }
        public string BorradoNombre { get; set; }
        public string BorradoEtiqueta { get; set; }

        public virtual Idioma Idioma { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}
