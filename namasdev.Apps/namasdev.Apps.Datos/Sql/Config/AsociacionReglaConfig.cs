using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AsociacionReglaConfig : EntityTypeConfiguration<AsociacionRegla>
    {
        public AsociacionReglaConfig()
        {
            ToTable(Entidades.Metadata.AsociacionReglaMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.AsociacionReglaMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.AsociacionReglaMetadata.Propiedades.Nombre.TAMAÑO_MAX);
        }
    }
}
