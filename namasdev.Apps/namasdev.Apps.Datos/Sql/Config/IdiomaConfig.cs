using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class IdiomaConfig : EntityTypeConfiguration<Idioma>
    {
        public IdiomaConfig()
        {
            ToTable(IdiomaMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(IdiomaMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(IdiomaMetadata.Propiedades.Nombre.TAMAÑO_MAX);

            HasRequired(p => p.EntidadPropiedadesMetadata)
                .WithRequiredDependent(p => p.Idioma);
        }
    }
}
