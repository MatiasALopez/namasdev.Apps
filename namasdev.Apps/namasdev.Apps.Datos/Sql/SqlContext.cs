﻿using System.Data.Entity;

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

        public DbSet<Aplicacion> Aplicaciones { get; set; }
        public DbSet<AplicacionVersion> AplicacionesVersiones { get; set; }
        public DbSet<PropiedadTipo> PropiedadTipos { get; set; }
        public DbSet<Entidad> Entidades { get; set; }
        public DbSet<EntidadPropiedad> EntidadesPropiedades { get; set; }
    }
}
