using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadIndicePropiedadConfig : EntityTypeConfiguration<EntidadIndicePropiedad>
    {
        public EntidadIndicePropiedadConfig()
        {
            ToTable(EntidadIndicePropiedadMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id).HasColumnName(EntidadIndicePropiedadMetadata.BD.ID);

            HasRequired(p => p.Indice)
                .WithMany(p => p.Propiedades)
                .HasForeignKey(p => p.EntidadIndiceId);

            HasRequired(p => p.Propiedad)
                .WithMany()
                .HasForeignKey(p => p.EntidadPropiedadId);
        }
    }
}
