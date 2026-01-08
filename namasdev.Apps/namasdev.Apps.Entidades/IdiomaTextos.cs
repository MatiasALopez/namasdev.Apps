using namasdev.Core.Entity;

namespace namasdev.Apps.Entidades
{
    public partial class IdiomaTextos : Entidad<string>
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
        public string BDNombre { get; set; }
        public string EntidadesNombre { get; set; }
        public string DatosNombre { get; set; }
        public string NegocioNombre { get; set; }
        public string WebPortalNombre { get; set; }
        public string Repositorio { get; set; }
        public string Agregar { get; set; }
        public string Actualizar { get; set; }
        public string Eliminar { get; set; }
        public string MarcarComoBorrado { get; set; }
        public string DesmarcarComoBorrado { get; set; }
        public string Parametros { get; set; }
        public string Propiedades { get; set; }

        public virtual Idioma Idioma { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}
