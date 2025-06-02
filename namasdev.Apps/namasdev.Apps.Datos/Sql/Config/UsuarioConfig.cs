using System.ComponentModel.DataAnnotations.Schema;

using namasdev.Data.Entity.Config;

using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class UsuarioConfig : EntidadBorradoConfigBase<Usuario>
    {
        public UsuarioConfig()
        {
            ToTable(UsuarioMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(UsuarioMetadata.BD.ID)
                .IsRequired()
                .HasMaxLength(UsuarioMetadata.Propiedades.UsuarioId.TAMAÑO_MAX);

            Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(UsuarioMetadata.Propiedades.Email.TAMAÑO_MAX);

            Property(e => e.Nombres)
                .IsRequired()
                .HasMaxLength(UsuarioMetadata.Propiedades.Nombres.TAMAÑO_MAX);

            Property(e => e.Apellidos)
                .IsRequired()
                .HasMaxLength(UsuarioMetadata.Propiedades.Apellidos.TAMAÑO_MAX);

            Property(e => e.ApellidosYNombres)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(e => e.NombresYApellidos)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            HasMany(s => s.Roles)
                .WithMany()
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("RoleId");
                    cs.ToTable("AspNetUserRoles");
                });
        }
    }
}
