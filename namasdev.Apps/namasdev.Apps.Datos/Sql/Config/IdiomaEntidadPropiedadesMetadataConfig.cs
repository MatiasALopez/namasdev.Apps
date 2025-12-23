using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class IdiomaEntidadPropiedadesMetadataConfig : EntityTypeConfiguration<IdiomaEntidadPropiedadesMetadata>
    {
        public IdiomaEntidadPropiedadesMetadataConfig()
        {
            ToTable(IdiomaEntidadPropiedadesMetadataMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(IdiomaEntidadPropiedadesMetadataMetadata.BD.ID);

            Property(p => p.CreadoPorNombre).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.CreadoPorNombre.TAMAÑO_MAX);
            Property(p => p.CreadoPorEtiqueta).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.CreadoPorEtiqueta.TAMAÑO_MAX);
            Property(p => p.CreadoFechaNombre).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.CreadoFechaNombre.TAMAÑO_MAX);
            Property(p => p.CreadoFechaEtiqueta).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.CreadoFechaEtiqueta.TAMAÑO_MAX);
            Property(p => p.UltimaModificacionPorNombre).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.UltimaModificacionPorNombre.TAMAÑO_MAX);
            Property(p => p.UltimaModificacionPorEtiqueta).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.UltimaModificacionPorEtiqueta.TAMAÑO_MAX);
            Property(p => p.UltimaModificacionFechaNombre).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.UltimaModificacionFechaNombre.TAMAÑO_MAX);
            Property(p => p.UltimaModificacionFechaEtiqueta).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.UltimaModificacionFechaEtiqueta.TAMAÑO_MAX);
            Property(p => p.BorradoPorNombre).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.BorradoPorNombre.TAMAÑO_MAX);
            Property(p => p.BorradoPorEtiqueta).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.BorradoPorEtiqueta.TAMAÑO_MAX);
            Property(p => p.BorradoFechaNombre).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.BorradoFechaNombre.TAMAÑO_MAX);
            Property(p => p.BorradoFechaEtiqueta).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.BorradoFechaEtiqueta.TAMAÑO_MAX);
            Property(p => p.BorradoNombre).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.BorradoNombre.TAMAÑO_MAX);
            Property(p => p.BorradoEtiqueta).IsRequired().HasMaxLength(IdiomaEntidadPropiedadesMetadataMetadata.Propiedades.BorradoEtiqueta.TAMAÑO_MAX);
        }
    }
}
