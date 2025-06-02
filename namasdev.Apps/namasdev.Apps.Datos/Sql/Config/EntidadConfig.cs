using namasdev.Data.Entity.Config;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadConfig : EntidadBorradoConfigBase<Entidad>
    {
        public EntidadConfig()
        {
            ToTable(EntidadMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(EntidadMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(EntidadMetadata.Propiedades.Nombre.TAMAÑO_MAX);

            Property(p => p.NombrePlural)
                .IsRequired()
                .HasMaxLength(EntidadMetadata.Propiedades.NombrePlural.TAMAÑO_MAX);

            Property(p => p.Etiqueta)
                .IsRequired()
                .HasMaxLength(EntidadMetadata.Propiedades.Etiqueta.TAMAÑO_MAX);

            Property(p => p.EtiquetaPlural)
                .IsRequired()
                .HasMaxLength(EntidadMetadata.Propiedades.EtiquetaPlural.TAMAÑO_MAX);

            HasRequired(p => p.AplicacionVersion)
                .WithMany(p => p.Entidades)
                .HasForeignKey(p => p.AplicacionVersionId);

            HasRequired(p => p.Especificaciones)
                .WithRequiredDependent(p => p.Entidad);
        }
    }
}
