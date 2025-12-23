using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class PropiedadCategoriaConfig : EntityTypeConfiguration<PropiedadCategoria>
    {
        public PropiedadCategoriaConfig()
        {
            ToTable(PropiedadCategoriaMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(PropiedadCategoriaMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(PropiedadCategoriaMetadata.Propiedades.Nombre.TAMAÑO_MAX);
        }
    }
}
