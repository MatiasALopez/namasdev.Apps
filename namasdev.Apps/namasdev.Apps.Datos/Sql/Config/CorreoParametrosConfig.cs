using System.Data.Entity.ModelConfiguration;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class CorreoParametrosConfig : EntityTypeConfiguration<CorreoParametros>
    {
        public CorreoParametrosConfig()
        {
            ToTable("CorreosParametros");
            HasKey(e => e.Id);

            Property(e => e.Asunto)
                .IsRequired()
                .HasMaxLength(256);

            Property(e => e.CopiaOculta)
                .HasMaxLength(100);
        }
    }
}
