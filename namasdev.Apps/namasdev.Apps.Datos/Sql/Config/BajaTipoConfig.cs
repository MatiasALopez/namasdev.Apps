using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class BajaTipoConfig : EntityTypeConfiguration<BajaTipo>
    {
        public BajaTipoConfig()
        {
            ToTable(BajaTipoMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(BajaTipoMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(BajaTipoMetadata.Propiedades.Nombre.TAMAÑO_MAX);
        }
    }
}
