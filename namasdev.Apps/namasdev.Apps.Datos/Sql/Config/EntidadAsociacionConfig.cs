using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadAsociacionConfig : EntityTypeConfiguration<EntidadAsociacion>
    {
        public EntidadAsociacionConfig()
        {
            ToTable(Entidades.Metadata.EntidadAsociacionMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.EntidadAsociacionMetadata.BD.ID);

            Property(p => p.TablaAuxiliarNombre)
                .HasMaxLength(Entidades.Metadata.EntidadAsociacionMetadata.Propiedades.TablaAuxiliarNombre.TAMAÑO_MAX);

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
