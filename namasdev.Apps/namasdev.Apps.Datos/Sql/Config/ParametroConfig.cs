using namasdev.Apps.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class ParametroConfig : EntityTypeConfiguration<Parametro>
    {
        public ParametroConfig()
        {
            ToTable(Entidades.Metadata.ParametroMetadata.BD.TABLA);
            HasKey(p => p.Nombre);

            Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.ParametroMetadata.Nombre.TAMAÑO_MAX);
        }
    }
}
