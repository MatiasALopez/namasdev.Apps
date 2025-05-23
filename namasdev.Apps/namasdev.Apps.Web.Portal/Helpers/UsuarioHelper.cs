﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Web.Portal.Helpers
{
	public class UsuarioHelper : namasdev.Web.Helpers.UsuarioHelper
	{
		private UsuariosRepositorio _usuariosRepositorio;

        public UsuarioHelper(HttpContextBase context)
			: base(context)
		{
			_usuariosRepositorio = new UsuariosRepositorio();

            CargarDatos();
		}

        private Usuario _usuario;
        public Usuario Usuario
        {
            get
            {
                if (_usuario == null)
                {
                    CargarDatos();
                }

                return _usuario;
            }
        }

		private void CargarDatos()
		{
			if (!UsuarioLogueado)
			{
				return;
			}

			_usuario = _usuariosRepositorio.Obtener(UsuarioId);
		}
    }
}