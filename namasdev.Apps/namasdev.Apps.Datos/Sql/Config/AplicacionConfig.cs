using namasdev.Data.Entity.Config;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AplicacionConfig : EntidadBorradoConfigBase<Aplicacion>
    {
        public AplicacionConfig()
        {
            ToTable(AplicacionMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(AplicacionMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(AplicacionMetadata.Propiedades.Nombre.TAMAÑO_MAX);
        }
    }
}
