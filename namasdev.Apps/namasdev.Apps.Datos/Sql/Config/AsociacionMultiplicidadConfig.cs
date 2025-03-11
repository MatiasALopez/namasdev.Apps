using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AsociacionMultiplicidadConfig : EntityTypeConfiguration<AsociacionMultiplicidad>
    {
        public AsociacionMultiplicidadConfig()
        {
            ToTable(Entidades.Metadata.AsociacionMultiplicidadMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.AsociacionMultiplicidadMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.AsociacionMultiplicidadMetadata.Nombre.TAMAÑO_MAX);
        }
    }
}
