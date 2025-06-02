using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AsociacionReglaConfig : EntityTypeConfiguration<AsociacionRegla>
    {
        public AsociacionReglaConfig()
        {
            ToTable(AsociacionReglaMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(AsociacionReglaMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(AsociacionReglaMetadata.Propiedades.Nombre.TAMAÑO_MAX);
        }
    }
}
