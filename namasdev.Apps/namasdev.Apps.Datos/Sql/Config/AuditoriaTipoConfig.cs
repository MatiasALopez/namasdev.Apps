using namasdev.Apps.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AuditoriaTipoConfig : EntityTypeConfiguration<AuditoriaTipo>
    {
        public AuditoriaTipoConfig()
        {
            ToTable(Entidades.Metadata.AuditoriaTipoMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.AuditoriaTipoMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.AuditoriaTipoMetadata.Propiedades.Nombre.TAMAÑO_MAX);
        }
    }
}
