using namasdev.Data.Entity.Config;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AplicacionVersionConfig : EntidadBorradoConfigBase<AplicacionVersion>
    {
        public AplicacionVersionConfig()
        {
            ToTable(AplicacionVersionMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(AplicacionVersionMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(AplicacionVersionMetadata.Propiedades.Nombre.TAMAÑO_MAX);

            HasRequired(p => p.Aplicacion)
                .WithMany()
                .HasForeignKey(p => p.AplicacionId);
        }
    }
}
