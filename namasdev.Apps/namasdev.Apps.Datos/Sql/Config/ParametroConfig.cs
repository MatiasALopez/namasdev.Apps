using namasdev.Apps.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class ParametroConfig : EntityTypeConfiguration<Parametro>
    {
        public ParametroConfig()
        {
            ToTable("Parametros");
            HasKey(e => e.Nombre);

            Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(75);
        }
    }
}
