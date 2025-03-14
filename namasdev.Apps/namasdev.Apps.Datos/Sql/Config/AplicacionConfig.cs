using namasdev.Apps.Entidades;
using namasdev.Data.Entity.Config;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AplicacionConfig : EntidadBorradoConfigBase<Aplicacion>
    {
        public AplicacionConfig()
        {
            ToTable(Entidades.Metadata.AplicacionMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.AplicacionMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.AplicacionMetadata.Propiedades.Nombre.TAMAÑO_MAX);
        }
    }
}
