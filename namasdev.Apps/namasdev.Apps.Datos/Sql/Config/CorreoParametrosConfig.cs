using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class CorreoParametrosConfig : EntityTypeConfiguration<CorreoParametros>
    {
        public CorreoParametrosConfig()
        {
            ToTable(CorreoParametrosMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(CorreoParametrosMetadata.BD.ID);

            Property(e => e.Asunto)
                .IsRequired()
                .HasMaxLength(CorreoParametrosMetadata.Propiedades.Asunto.TAMAÑO_MAX);
        }
    }
}
