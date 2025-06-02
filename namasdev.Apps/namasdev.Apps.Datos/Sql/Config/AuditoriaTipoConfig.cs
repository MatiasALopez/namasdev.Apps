using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AuditoriaTipoConfig : EntityTypeConfiguration<AuditoriaTipo>
    {
        public AuditoriaTipoConfig()
        {
            ToTable(AuditoriaTipoMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(AuditoriaTipoMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(AuditoriaTipoMetadata.Propiedades.Nombre.TAMAÑO_MAX);
        }
    }
}
