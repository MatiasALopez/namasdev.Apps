using namasdev.Apps.Entidades;
using namasdev.Data.Entity.Config;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class AplicacionConfig : EntidadBorradoConfigBase<Aplicacion>
    {
        public AplicacionConfig()
        {
            ToTable("Aplicaciones");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("AplicacionId");

            Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
