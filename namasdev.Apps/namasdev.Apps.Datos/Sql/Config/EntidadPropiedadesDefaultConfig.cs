using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadPropiedadesDefaultConfig : EntityTypeConfiguration<EntidadPropiedadesDefault>
    {
        public EntidadPropiedadesDefaultConfig()
        {
            ToTable(Entidades.Metadata.EntidadPropiedadesDefaultMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.EntidadPropiedadesDefaultMetadata.BD.ID);

            HasOptional(p => p.IDTipo)
                .WithMany()
                .HasForeignKey(p => p.IDPropiedadTipoId);
        }
    }
}
