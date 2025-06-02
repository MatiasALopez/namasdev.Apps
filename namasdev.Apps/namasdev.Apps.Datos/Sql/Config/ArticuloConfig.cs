using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class ArticuloConfig : EntityTypeConfiguration<Articulo>
    {
        public ArticuloConfig()
        {
            ToTable(ArticuloMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(ArticuloMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(ArticuloMetadata.Propiedades.Nombre.TAMAÑO_MAX);
        }
    }
}
