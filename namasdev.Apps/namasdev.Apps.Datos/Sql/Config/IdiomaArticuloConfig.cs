using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class IdiomaArticuloConfig : EntityTypeConfiguration<IdiomaArticulo>
    {
        public IdiomaArticuloConfig()
        {
            ToTable(IdiomaArticuloMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(IdiomaArticuloMetadata.BD.ID)
                .HasMaxLength(IdiomaArticuloMetadata.Propiedades.Id.TAMAÑO_EXACTO);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(IdiomaArticuloMetadata.Propiedades.Nombre.TAMAÑO_MAX);

            HasRequired(p => p.Idioma)
                .WithMany(p => p.Articulos)
                .HasForeignKey(p => p.IdiomaId);
        }
    }
}
