using System.ComponentModel.DataAnnotations.Schema;
using namasdev.Apps.Entidades;
using namasdev.Data.Entity.Config;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class UsuarioConfig : EntidadBorradoConfigBase<Usuario>
    {
        public UsuarioConfig()
        {
            ToTable("Usuarios");
            HasKey(e => e.Id);

            Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("UsuarioId");

            Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256);

            Property(e => e.Nombres)
                .IsRequired()
                .HasMaxLength(100);

            Property(e => e.Apellidos)
                .IsRequired()
                .HasMaxLength(100);

            Property(e => e.NombreCompleto)
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
