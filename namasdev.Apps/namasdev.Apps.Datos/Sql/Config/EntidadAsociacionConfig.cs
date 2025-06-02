using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadAsociacionConfig : EntityTypeConfiguration<EntidadAsociacion>
    {
        public EntidadAsociacionConfig()
        {
            ToTable(EntidadAsociacionMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(EntidadAsociacionMetadata.BD.ID);

            Property(p => p.Nombre)
                .HasMaxLength(EntidadAsociacionMetadata.Propiedades.Nombre.TAMAÑO_MAX);

            Property(p => p.OrigenEntidadPropiedadNavegacionNombre)
                .HasMaxLength(EntidadAsociacionMetadata.Propiedades.OrigenEntidadPropiedadNavegacionNombre.TAMAÑO_MAX);

            Property(p => p.DestinoEntidadPropiedadNavegacionNombre)
                .HasMaxLength(EntidadAsociacionMetadata.Propiedades.DestinoEntidadPropiedadNavegacionNombre.TAMAÑO_MAX);

            Property(p => p.TablaAuxiliarNombre)
                .HasMaxLength(EntidadAsociacionMetadata.Propiedades.TablaAuxiliarNombre.TAMAÑO_MAX);

            HasRequired(p => p.OrigenEntidad)
                .WithMany(p => p.AsociacionesOrigen)
                .HasForeignKey(p => p.OrigenEntidadId);

            HasRequired(p => p.OrigenPropiedad)
                .WithMany()
                .HasForeignKey(p => p.OrigenEntidadPropiedadId);

            HasRequired(p => p.OrigenMultiplicidad)
                .WithMany()
                .HasForeignKey(p => p.OrigenAsociacionMultiplicidadId);

            HasRequired(p => p.DestinoEntidad)
                .WithMany(p => p.AsociacionesDestino)
                .HasForeignKey(p => p.DestinoEntidadId);

            HasRequired(p => p.DestinoPropiedad)
                .WithMany()
                .HasForeignKey(p => p.DestinoEntidadPropiedadId);

            HasRequired(p => p.DestinoMultiplicidad)
                .WithMany()
                .HasForeignKey(p => p.DestinoAsociacionMultiplicidadId);

            HasRequired(p => p.DeleteRegla)
                .WithMany()
                .HasForeignKey(p => p.DeleteAsociacionReglaId);

            HasRequired(p => p.UpdateRegla)
                .WithMany()
                .HasForeignKey(p => p.UpdateAsociacionReglaId);
        }
    }
}
