using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class PropiedadTipoConfig : EntityTypeConfiguration<PropiedadTipo>
    {
        public PropiedadTipoConfig()
        {
            ToTable(PropiedadTipoMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(PropiedadTipoMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(PropiedadTipoMetadata.Propiedades.Nombre.TAMAÑO_MAX);

            Property(p => p.CLRType)
                .IsRequired()
                .HasMaxLength(PropiedadTipoMetadata.Propiedades.CLRType.TAMAÑO_MAX);

            Property(p => p.TSQLType)
                .IsRequired()
                .HasMaxLength(PropiedadTipoMetadata.Propiedades.TSQLType.TAMAÑO_MAX);
        }
    }
}
