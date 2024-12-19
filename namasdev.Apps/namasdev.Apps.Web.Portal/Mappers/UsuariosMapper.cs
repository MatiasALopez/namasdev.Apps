using namasdev.Apps.Entidades;
using namasdev.Apps.Web.Portal.Models.Usuarios;
using namasdev.Apps.Web.Portal.ViewModels.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace namasdev.Apps.Web.Portal.Mappers
{
    public class UsuariosMapper
    {
        public static List<UsuarioItemModel> MapearUsuariosEntidadesAModelos(
            List<Usuario> entidades,
            List<string> idsNoActivados)
        {
            if (entidades == null)
            {
                throw new ArgumentNullException("entidades");
            }

            return entidades
                .Select(e => new UsuarioItemModel
                {
                    UsuarioId = e.Id,
                    Nombres = e.Nombres,
                    Apellidos = e.Apellidos,
                    Email = e.Email,
                    Activado = !idsNoActivados.Contains(e.Id),
                    Rol = MapearUsuarioRol(e),
                })
                .ToList();
        }

        public static Usuario MapearUsuarioViewModelAEntidad(UsuarioViewModel modelo)
        {
            if (modelo == null)
            {
                throw new ArgumentNullException("modelo");
            }

            return new Usuario
            {
                Id = modelo.UsuarioId,
                Nombres = modelo.Nombres,
                Apellidos = modelo.Apellidos,
                Email = modelo.Email,
            };
        }

        public static UsuarioViewModel MapearUsuarioEntidadAViewModel(Usuario entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException("entidad");
            }

            return new UsuarioViewModel
            {
                UsuarioId = entidad.Id,
                Nombres = entidad.Nombres,
                Apellidos = entidad.Apellidos,
                Email = entidad.Email,
                Rol = MapearUsuarioRol(entidad)
            };
        }

        private static string MapearUsuarioRol(Usuario entidad)
        {
            return entidad.Roles?.Select(r => r.Name).FirstOrDefault();
        }
    }
}