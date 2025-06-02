using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadClaveConfig : EntityTypeConfiguration<EntidadClave>
    {
        public EntidadClaveConfig()
        {
            ToTable(EntidadClaveMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(EntidadClaveMetadata.BD.ID);

            HasRequired(p => p.Entidad)
                .WithMany(p => p.Claves)
                .HasForeignKey(p => p.EntidadId);

            HasRequired(p => p.Propiedad)
                .WithMany()
                .HasForeignKey(p => p.EntidadPropiedadId);
        }
    }
}
