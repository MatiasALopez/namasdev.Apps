using namasdev.Apps.Entidades;
using namasdev.Data.Entity.Config;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AplicacionVersionConfig : EntidadBorradoConfigBase<AplicacionVersion>
    {
        public AplicacionVersionConfig()
        {
            ToTable(Entidades.Metadata.AplicacionVersionMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.AplicacionVersionMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.AplicacionVersionMetadata.Nombre.TAMAÑO_MAX);

            HasRequired(p => p.Aplicacion)
                .WithMany()
                .HasForeignKey(p => p.AplicacionId);
        }
    }
}
