using namasdev.Apps.Entidades;
using namasdev.Data.Entity.Config;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadConfig : EntidadBorradoConfigBase<Entidad>
    {
        public EntidadConfig()
        {
            ToTable(Entidades.Metadata.EntidadMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.EntidadMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.EntidadMetadata.Nombre.TAMAÑO_MAX);

            HasRequired(p => p.AplicacionVersion)
                .WithMany(p => p.Entidades)
                .HasForeignKey(p => p.AplicacionVersionId);
        }
    }
}
