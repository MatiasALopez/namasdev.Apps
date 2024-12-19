﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;
using namasdev.Core.Types;
using namasdev.Data.Entity;

namespace namasdev.Apps.Datos
{
    public class UsuariosRepositorio : Repositorio<SqlContext, Usuario, string>, IUsuariosRepositorio
    {
        public List<string> ObtenerEmailsPorRol(string rolNombre)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.Usuarios
                    .Where(u =>
                        u.Roles.Any(r => r.Name == rolNombre)
                        && !u.Borrado)
                    .Select(u => u.Email)
                    .Distinct()
                    .ToList();
            }
        }

        public string ObtenerNombreCompletoPorId(string usuarioId)
        {
            using (var ctx = new SqlContext())
            {
                return ctx.Usuarios
                    .Where(u => u.Id == usuarioId && !u.Borrado)
                    .Select(u => u.NombreCompleto)
                    .FirstOrDefault();
            }
        }

        public Usuario Obtener(string usuarioId,
            bool cargarRoles = false)
        {
            using (var ctx = new SqlContext())
            {
                var query = ctx.Usuarios.AsQueryable();

                if (cargarRoles)
                {
                    query = query.Include(u => u.Roles);
                }

                return query
                    .FirstOrDefault(e => e.Id == usuarioId && !e.Borrado);
            }
        }

        public bool ExisteBorradoPorEmail(string email, 
            out string usuarioId)
        {
            using (var ctx = new SqlContext())
            {
                usuarioId = ctx.Usuarios
                    .Where(u => u.Email == email && u.Borrado)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                return !String.IsNullOrWhiteSpace(usuarioId);
            }
        }

        public List<AspNetRole> ObtenerRoles()
        {
            using (var ctx = new Sql.SqlContext())
            {
                return ctx.AspNetRoles
                    .OrderBy(r => r.Name)
                    .ToList();
            }
        }

        public List<Usuario> ObtenerListado(
            string busqueda = null, string rol = null,
            bool cargarRoles = false,
            OrdenYPaginacionParametros op = null)
        {
            using (var ctx = new Sql.SqlContext())
            {
                var query = ctx.Usuarios.AsQueryable();

                if (cargarRoles)
                {
                    query = query.Include(u => u.Roles);
                }

                if (!String.IsNullOrWhiteSpace(busqueda))
                {
                    query = query.Where(u =>
                        u.NombreCompleto.Contains(busqueda)
                        || u.Email.Contains(busqueda));
                }

                if (!String.IsNullOrWhiteSpace(rol))
                {
                    query = query.Where(u => u.Roles.Any(r => r.Name == rol));
                }

                return query
                    .Where(u => !u.Borrado)
                    .OrdenarYPaginar(op, ordenDefault: nameof(Usuario.NombreCompleto))
                    .ToList();
            }
        }

        public string ObtenerRolDeUsuario(string usuarioId)
        {
            using (var ctx = new Sql.SqlContext())
            {
                return ctx.Usuarios
                    .Where(u => u.Id == usuarioId)
                    .Select(u => u.Roles.Select(ur => ur.Name).FirstOrDefault())
                    .FirstOrDefault();
            }
        }
    }
}
