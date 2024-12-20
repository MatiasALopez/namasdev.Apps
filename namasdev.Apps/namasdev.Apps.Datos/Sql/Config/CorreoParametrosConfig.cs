using System.Data.Entity.ModelConfiguration;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class CorreoParametrosConfig : EntityTypeConfiguration<CorreoParametros>
    {
        public CorreoParametrosConfig()
        {
            ToTable(Entidades.Metadata.CorreoParametrosMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.CorreoParametrosMetadata.BD.ID);

            Property(e => e.Asunto)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.CorreoParametrosMetadata.Asunto.TAMAÑO_MAX);
        }
    }
}
