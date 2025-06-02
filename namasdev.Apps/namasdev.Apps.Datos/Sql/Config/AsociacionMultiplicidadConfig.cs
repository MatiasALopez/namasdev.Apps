using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AsociacionMultiplicidadConfig : EntityTypeConfiguration<AsociacionMultiplicidad>
    {
        public AsociacionMultiplicidadConfig()
        {
            ToTable(AsociacionMultiplicidadMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(AsociacionMultiplicidadMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(AsociacionMultiplicidadMetadata.Propiedades.Nombre.TAMAÑO_MAX);
        }
    }
}
