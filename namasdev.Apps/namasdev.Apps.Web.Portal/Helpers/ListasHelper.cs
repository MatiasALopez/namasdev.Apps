using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Web.Portal.Helpers
{
    public class ListasHelper
    {
        public static SelectList ObtenerIdiomasSelectList(IEnumerable<Idioma> idiomas)
        {
            return namasdev.Web.Helpers.ListasHelper.CrearSelectListDesdeLista(idiomas,
                m => new SelectListItem
                {
                    Text = m.Nombre,
                    Value = m.Id.ToString()
                });
        }

        public static SelectList ObtenerAsociacionMultiplicidadesSelectList(IEnumerable<AsociacionMultiplicidad> multiplicidades)
        {
            return namasdev.Web.Helpers.ListasHelper.CrearSelectListDesdeLista(multiplicidades, 
                m => new SelectListItem
                {
                    Text = m.Nombre,
                    Value = m.Id.ToString()
                });
        }

        public static SelectList ObtenerAsociacionReglasSelectList(IEnumerable<AsociacionRegla> reglas)
        {
            return namasdev.Web.Helpers.ListasHelper.CrearSelectListDesdeLista(reglas,
                r => new SelectListItem
                {
                    Text = r.Nombre,
                    Value = r.Id.ToString()
                });
        }

        public static SelectList ObtenerEntidadesSelectList(IEnumerable<Entidad> entidades)
        {
            return namasdev.Web.Helpers.ListasHelper.CrearSelectListDesdeItems(ObtenerEntidadesSelectListItems(entidades));
        }

        public static IEnumerable<SelectListItem> ObtenerEntidadesSelectListItems(IEnumerable<Entidad> entidades)
        {
            return entidades
                .Select(e => new SelectListItem 
                {
                    Text = e.Nombre,
                    Value = e.Id.ToString()
                })
                .ToArray();
        }

        public static SelectList ObtenerEntidadesPropiedadesSelectList(IEnumerable<EntidadPropiedad> propiedades)
        {
            return namasdev.Web.Helpers.ListasHelper.CrearSelectListDesdeItems(ObtenerEntidadesPropiedadesSelectListItems(propiedades));
        }

        public static IEnumerable<SelectListItem> ObtenerEntidadesPropiedadesSelectListItems(IEnumerable<EntidadPropiedad> propiedades)
        {
            return propiedades
                .Select(p => new SelectListItem
                {
                    Text = p.Nombre,
                    Value = p.Id.ToString()
                })
                .ToArray();
        }

        public static SelectList ObtenerPropiedadTiposSelectList(IEnumerable<PropiedadTipo> tipos)
        {
            return namasdev.Web.Helpers.ListasHelper.CrearSelectListDesdeLista(
                tipos,
                pt => new SelectListItem
                {
                    Text = pt.Nombre,
                    Value = pt.Id.ToString()
                });
        }

        public static SelectList ObtenerArticulosSelectList(IEnumerable<IdiomaArticulo> articulos)
        {
            return namasdev.Web.Helpers.ListasHelper.CrearSelectListDesdeLista(
                articulos,
                a => new SelectListItem
                {
                    Text = a.Nombre,
                    Value = a.Id.ToString()
                });
        }

        public static SelectList ObtenerBajaTiposSelectList(IEnumerable<BajaTipo> tipos)
        {
            return namasdev.Web.Helpers.ListasHelper.CrearSelectListDesdeLista(
                tipos,
                bt => new SelectListItem
                {
                    Text = bt.Nombre,
                    Value = bt.Id.ToString()
                });
        }

        public static SelectList ObtenerVersionesSelectList(IEnumerable<AplicacionVersion> versiones)
        {
            return namasdev.Web.Helpers.ListasHelper.CrearSelectListDesdeLista(
                versiones,
                v => new SelectListItem
                {
                    Text = v.Nombre,
                    Value = v.Id.ToString()
                });
        }

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