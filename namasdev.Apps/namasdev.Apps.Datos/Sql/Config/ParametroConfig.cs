using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class ParametroConfig : EntityTypeConfiguration<Parametro>
    {
        public ParametroConfig()
        {
            ToTable(ParametroMetadata.BD.TABLA);
            HasKey(p => p.Nombre);

            Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(ParametroMetadata.Propiedades.Nombre.TAMAÑO_MAX);
        }
    }
}
