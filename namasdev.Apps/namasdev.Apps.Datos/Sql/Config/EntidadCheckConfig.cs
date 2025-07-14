using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadCheckConfig : EntityTypeConfiguration<EntidadCheck>
    {
        public EntidadCheckConfig()
        {
            ToTable(EntidadCheckMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(EntidadCheckMetadata.BD.ID);

            Property(p => p.Nombre)
                .HasMaxLength(EntidadCheckMetadata.Propiedades.Nombre.TAMAÑO_MAX);

            Property(p => p.Expresion)
                .HasMaxLength(EntidadCheckMetadata.Propiedades.ExpresionNombre.TAMAÑO_MAX);

            HasRequired(p => p.Entidad)
                .WithMany(p => p.Checks)
                .HasForeignKey(p => p.EntidadId);
        }
    }
}
