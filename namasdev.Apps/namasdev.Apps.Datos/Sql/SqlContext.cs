using System;
using System.Data.Entity;
using System.Data.SqlClient;
using namasdev.Apps.Entidades;
using namasdev.Data.Entity;

namespace namasdev.Apps.Datos.Sql
{
    public class SqlContext : DbContextBase
    {
        public SqlContext()
            : base("name=namasdev")
        {
        }

        public DbSet<AspNetRole> AspNetRoles { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CorreoParametros> CorreosParametros { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }
        public DbSet<Aplicacion> Aplicaciones { get; set; }
        public DbSet<AplicacionVersion> AplicacionesVersiones { get; set; }
        public DbSet<PropiedadTipo> PropiedadTipos { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<BajaTipo> BajaTipos { get; set; }
        public DbSet<Entidad> Entidades { get; set; }
        public DbSet<EntidadEspecificaciones> EntidadesEspecificaciones { get; set; }
        public DbSet<EntidadPropiedad> EntidadesPropiedades { get; set; }
        public DbSet<EntidadClave> EntidadesClaves { get; set; }
        public DbSet<AsociacionMultiplicidad> AsociacionMultiplicidades { get; set; }
        public DbSet<AsociacionRegla> AsociacionReglas { get; set; }
        public DbSet<EntidadAsociacion> EntidadesAsociaciones { get; set; }
        public DbSet<EntidadIndice> EntidadesIndices { get; set; }
        public DbSet<EntidadIndicePropiedad> EntidadesIndicesPropiedades { get; set; }
        public DbSet<EntidadCheck> EntidadesChecks { get; set; }

        public void uspClonarAplicacionVersion(Guid aplicacionVersionIdOrigen, Guid aplicacionVersionIdDestino, string usuarioId, DateTime fechaHora)
        {
            EjecutarComando(
                "EXEC dbo.uspClonarAplicacionVersion @AplicacionVersionIdOrigen,@AplicacionVersionIdDestino,@UsuarioId,@FechaHora",
                parametros: new SqlParameter[] 
                {
                    new SqlParameter("@AplicacionVersionIdOrigen", aplicacionVersionIdOrigen),
                    new SqlParameter("@AplicacionVersionIdDestino", aplicacionVersionIdDestino),
                    new SqlParameter("@UsuarioId", usuarioId),
                    new SqlParameter("@FechaHora", fechaHora)
                });
        }
    }
}
