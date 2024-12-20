using namasdev.Apps.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class PropiedadTipoConfig : EntityTypeConfiguration<PropiedadTipo>
    {
        public PropiedadTipoConfig()
        {
            ToTable(Entidades.Metadata.PropiedadTipoMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.PropiedadTipoMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.PropiedadTipoMetadata.Nombre.TAMAÑO_MAX);
        }
    }
}
