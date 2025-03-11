using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadClaveConfig : EntityTypeConfiguration<EntidadClave>
    {
        public EntidadClaveConfig()
        {
            ToTable(Entidades.Metadata.EntidadClaveMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.EntidadClaveMetadata.BD.ID);

            HasRequired(p => p.Entidad)
                .WithMany(p => p.Claves)
                .HasForeignKey(p => p.EntidadId);

            HasRequired(p => p.Propiedad)
                .WithMany()
                .HasForeignKey(p => p.EntidadPropiedadId);
        }
    }
}
