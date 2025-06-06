using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadPropiedadConfig : EntityTypeConfiguration<EntidadPropiedad>
    {
        public EntidadPropiedadConfig()
        {
            ToTable(EntidadPropiedadMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id).HasColumnName(EntidadPropiedadMetadata.BD.ID);
            Property(p => p.Nombre).IsRequired().HasMaxLength(EntidadPropiedadMetadata.Propiedades.Nombre.TAMAÑO_MAX);
            Property(p => p.Etiqueta).IsRequired().HasMaxLength(EntidadPropiedadMetadata.Propiedades.Etiqueta.TAMAÑO_MAX);

            HasRequired(p => p.Entidad).WithMany(p => p.Propiedades).HasForeignKey(p => p.EntidadId);
            HasRequired(p => p.Tipo).WithMany().HasForeignKey(p => p.PropiedadTipoId);
        }
    }
}
