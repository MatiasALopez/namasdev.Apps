﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Web.Portal.Helpers
{
    public class ListasHelper
    {
        public static SelectList ObtenerRolesSelectList(IEnumerable<AspNetRole> roles)
        {
            return namasdev.Web.Helpers.ListasHelper.CrearSelectListDesdeItems(ObtenerRolesSelectListItems(roles));
        }

        public static IEnumerable<SelectListItem> ObtenerRolesSelectListItems(
            IEnumerable<AspNetRole> roles,
            IEnumerable<string> seleccionados = null)
        {
            return roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = seleccionados != null && seleccionados.Contains(r.Name)
                });
        }        
    }
}