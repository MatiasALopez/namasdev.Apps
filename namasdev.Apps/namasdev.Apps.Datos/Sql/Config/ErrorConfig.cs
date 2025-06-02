using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class ErrorConfig : EntityTypeConfiguration<Error>
    {
        public ErrorConfig()
        {
            ToTable(ErrorMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(ErrorMetadata.BD.ID);

            Property(e => e.Mensaje)
                .IsRequired();

            Property(e => e.StackTrace)
                .IsRequired();

            Property(e => e.Source)
                .IsRequired()
                .HasMaxLength(ErrorMetadata.Propiedades.Source.TAMAÑO_MAX);

            Property(e => e.UserId)
                .HasMaxLength(ErrorMetadata.Propiedades.UserId.TAMAÑO_MAX);
        }
    }
}
