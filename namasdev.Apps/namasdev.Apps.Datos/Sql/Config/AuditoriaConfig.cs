using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AuditoriaConfig : EntityTypeConfiguration<Auditoria>
    {
        public AuditoriaConfig()
        {
            ToTable(AuditoriaMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(AuditoriaMetadata.BD.ID);

            Property(p => p.Tabla)
                .IsRequired()
                .HasMaxLength(AuditoriaMetadata.Propiedades.Tabla.TAMAÑO_MAX);

            HasRequired(p => p.Tipo)
                .WithMany()
                .HasForeignKey(p => p.AuditoriaTipoId);
        }
    }
}
