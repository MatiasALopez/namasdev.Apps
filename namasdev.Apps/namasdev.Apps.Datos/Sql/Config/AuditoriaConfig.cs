using namasdev.Apps.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AuditoriaConfig : EntityTypeConfiguration<Auditoria>
    {
        public AuditoriaConfig()
        {
            ToTable(Entidades.Metadata.AuditoriaMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.AuditoriaMetadata.BD.ID);

            Property(p => p.Tabla)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.AuditoriaMetadata.Propiedades.Tabla.TAMAÑO_MAX);

            HasRequired(p => p.Tipo)
                .WithMany()
                .HasForeignKey(p => p.AuditoriaTipoId);
        }
    }
}
