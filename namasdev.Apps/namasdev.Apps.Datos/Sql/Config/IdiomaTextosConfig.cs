using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class IdiomaTextosConfig : EntityTypeConfiguration<IdiomaTextos>
    {
        public IdiomaTextosConfig()
        {
            ToTable(IdiomaTextosMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(IdiomaTextosMetadata.BD.ID);

            Property(p => p.CreadoPorNombre).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.CreadoPorNombre.TAMAÑO_MAX);
            Property(p => p.CreadoPorEtiqueta).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.CreadoPorEtiqueta.TAMAÑO_MAX);
            Property(p => p.CreadoFechaNombre).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.CreadoFechaNombre.TAMAÑO_MAX);
            Property(p => p.CreadoFechaEtiqueta).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.CreadoFechaEtiqueta.TAMAÑO_MAX);
            Property(p => p.UltimaModificacionPorNombre).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.UltimaModificacionPorNombre.TAMAÑO_MAX);
            Property(p => p.UltimaModificacionPorEtiqueta).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.UltimaModificacionPorEtiqueta.TAMAÑO_MAX);
            Property(p => p.UltimaModificacionFechaNombre).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.UltimaModificacionFechaNombre.TAMAÑO_MAX);
            Property(p => p.UltimaModificacionFechaEtiqueta).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.UltimaModificacionFechaEtiqueta.TAMAÑO_MAX);
            Property(p => p.BorradoPorNombre).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.BorradoPorNombre.TAMAÑO_MAX);
            Property(p => p.BorradoPorEtiqueta).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.BorradoPorEtiqueta.TAMAÑO_MAX);
            Property(p => p.BorradoFechaNombre).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.BorradoFechaNombre.TAMAÑO_MAX);
            Property(p => p.BorradoFechaEtiqueta).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.BorradoFechaEtiqueta.TAMAÑO_MAX);
            Property(p => p.BorradoNombre).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.BorradoNombre.TAMAÑO_MAX);
            Property(p => p.BorradoEtiqueta).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.BorradoEtiqueta.TAMAÑO_MAX);
            Property(p => p.BDNombre).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.BDNombre.TAMAÑO_MAX);
            Property(p => p.EntidadesNombre).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.EntidadesNombre.TAMAÑO_MAX);
            Property(p => p.DatosNombre).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.DatosNombre.TAMAÑO_MAX);
            Property(p => p.NegocioNombre).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.NegocioNombre.TAMAÑO_MAX);
            Property(p => p.WebPortalNombre).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.WebPortalNombre.TAMAÑO_MAX);
            Property(p => p.Repositorio).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.Repositorio.TAMAÑO_MAX);
            Property(p => p.Agregar).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.Agregar.TAMAÑO_MAX);
            Property(p => p.Actualizar).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.Actualizar.TAMAÑO_MAX);
            Property(p => p.Eliminar).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.Eliminar.TAMAÑO_MAX);
            Property(p => p.MarcarComoBorrado).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.MarcarComoBorrado.TAMAÑO_MAX);
            Property(p => p.DesmarcarComoBorrado).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.DesmarcarComoBorrado.TAMAÑO_MAX);
            Property(p => p.Parametros).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades.Parametros.TAMAÑO_MAX);
            Property(p => p.Propiedades).IsRequired().HasMaxLength(IdiomaTextosMetadata.Propiedades._Propiedades.TAMAÑO_MAX);
        }
    }
}
