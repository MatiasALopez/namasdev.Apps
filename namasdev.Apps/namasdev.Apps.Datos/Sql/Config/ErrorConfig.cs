using System.Data.Entity.ModelConfiguration;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class ErrorConfig : EntityTypeConfiguration<Error>
    {
        public ErrorConfig()
        {
            ToTable("Errores");
            HasKey(e => e.Id);
            
            Property(e => e.Mensaje)
                .IsRequired();

            Property(e => e.StackTrace)
                .IsRequired();

            Property(e => e.Source)
                .IsRequired()
                .HasMaxLength(200);

            Property(e => e.UserId)
                .HasMaxLength(128);
        }
    }
}
