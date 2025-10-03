using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadIndiceConfig : EntityTypeConfiguration<EntidadIndice>
    {
        public EntidadIndiceConfig()
        {
            ToTable(EntidadIndiceMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(EntidadIndiceMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(EntidadIndiceMetadata.Propiedades.Nombre.TAMAÑO_MAX);

            Property(p => p.Condiciones)
                .HasMaxLength(EntidadIndiceMetadata.Propiedades.Condiciones.TAMAÑO_MAX);

            HasRequired(p => p.Entidad)
                .WithMany(p => p.Indices)
                .HasForeignKey(p => p.EntidadId);
        }
    }
}
