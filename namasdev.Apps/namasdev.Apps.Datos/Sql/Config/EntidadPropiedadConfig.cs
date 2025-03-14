using namasdev.Apps.Entidades;
using namasdev.Data.Entity.Config;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadPropiedadConfig : EntidadBorradoConfigBase<EntidadPropiedad>
    {
        public EntidadPropiedadConfig()
        {
            ToTable(Entidades.Metadata.EntidadPropiedadMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.EntidadPropiedadMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.EntidadPropiedadMetadata.Propiedades.Nombre.TAMAÑO_MAX);

            Property(p => p.Etiqueta)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.EntidadPropiedadMetadata.Propiedades.Etiqueta.TAMAÑO_MAX);

            Property(p => p.CalculadaFormula)
                .HasMaxLength(Entidades.Metadata.EntidadPropiedadMetadata.Propiedades.CalculadaFormula.TAMAÑO_MAX);

            HasRequired(p => p.Entidad)
                .WithMany(p => p.Propiedades)
                .HasForeignKey(p => p.EntidadId);

            HasRequired(p => p.Tipo)
                .WithMany()
                .HasForeignKey(p => p.PropiedadTipoId);
        }
    }
}
