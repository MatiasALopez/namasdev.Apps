using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadIndiceConfig : EntityTypeConfiguration<EntidadIndice>
    {
        public EntidadIndiceConfig()
        {
            ToTable(Entidades.Metadata.EntidadIndiceMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.EntidadIndiceMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.EntidadIndiceMetadata.Nombre.TAMAÑO_MAX);

            HasRequired(p => p.Entidad)
                .WithMany(p => p.Indices)
                .HasForeignKey(p => p.EntidadId);
        }
    }
}
