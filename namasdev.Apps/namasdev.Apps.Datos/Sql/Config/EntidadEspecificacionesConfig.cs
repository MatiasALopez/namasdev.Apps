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

            Property(p => p.IdiomaArticuloId)
                .HasMaxLength(EntidadEspecificacionesMetadata.Propiedades.IdiomaArticuloId.TAMAÑO_EXACTO);

            HasOptional(p => p.IdiomaArticulo)
                .WithMany()
                .HasForeignKey(p => p.IdiomaArticuloId);

            HasOptional(p => p.IDTipo)
                .WithMany()
                .HasForeignKey(p => p.IDPropiedadTipoId);

            HasRequired(p => p.BajaTipo)
                .WithMany()
                .HasForeignKey(p => p.BajaTipoId);
        }
    }
}
