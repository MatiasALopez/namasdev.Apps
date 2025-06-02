using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadEspecificacionesConfig : EntityTypeConfiguration<EntidadEspecificaciones>
    {
        public EntidadEspecificacionesConfig()
        {
            ToTable(EntidadEspecificacionesMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(EntidadEspecificacionesMetadata.BD.ID);

            HasRequired(p => p.Articulo)
                .WithMany()
                .HasForeignKey(p => p.ArticuloId);

            HasOptional(p => p.IDTipo)
                .WithMany()
                .HasForeignKey(p => p.IDPropiedadTipoId);

            HasRequired(p => p.BajaTipo)
                .WithMany()
                .HasForeignKey(p => p.BajaTipoId);
        }
    }
}
