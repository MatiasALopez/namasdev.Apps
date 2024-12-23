using System;
using System.Linq;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using namasdev.Core.Validation;
using namasdev.Web.Helpers;
using namasdev.Web.Models;
using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio;
using namasdev.Apps.Web.Portal.Mappers;
using namasdev.Apps.Web.Portal.Models;
using namasdev.Apps.Web.Portal.ViewModels.Usuarios;

namespace namasdev.Apps.Web.Portal.Controllers
{
    [Authorize(Roles = AspNetRoles.ADMINISTRADOR)]
    public class UsuariosController : ControllerBase
    {
        private const string USUARIO_VIEW_NAME = "Usuario";

        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IUsuariosNegocio _usuariosNegocio;
        private readonly ICorreosNegocio _correosNegocio;

        public UsuariosController(IUsuariosRepositorio usuariosRepositorio, IUsuariosNegocio usuariosNegocio, ICorreosNegocio correosNegocio)
        {
            Validador.ValidarArgumentRequeridoYThrow(usuariosRepositorio, nameof(usuariosRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(usuariosNegocio, nameof(usuariosNegocio));
            Validador.ValidarArgumentRequeridoYThrow(correosNegocio, nameof(correosNegocio));

            _usuariosRepositorio = usuariosRepositorio;
            _usuariosNegocio = usuariosNegocio;
            _correosNegocio = correosNegocio;
        }

        #region Acciones

        public ActionResult Index(
            string rol = null, string busqueda = null,
            string orden = null,
            int pagina = 1)
        {
            var modelo = new UsuariosViewModel 
            { 
                Rol = rol,
                Busqueda = busqueda,
                Pagina = pagina,
                Orden = orden,
            };

            var op = modelo.CrearOrdenYPaginacionParametros();

            modelo.Items = UsuariosMapper.MapearUsuariosEntidadesAModelos(
                entidades: _usuariosRepositorio.ObtenerListado(
                    busqueda: modelo.Busqueda, rol: modelo.Rol,
                    cargarRoles: true,
                    op: op),
                idsNoActivados: UserManager.ObtenerIdsUsuariosNoActivados());

            modelo.CargarPaginacion(op);

            CargarUsuariosViewModel(modelo);
            return View(modelo);
        }

        public ActionResult Agregar()
        {
            var modelo = new UsuarioViewModel();
            CargarUsuarioViewModel(modelo, PaginaModo.Agregar);

            return View(USUARIO_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Agregar(UsuarioViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = string.Empty;
                    if (_usuariosRepositorio.ExisteBorradoPorEmail(modelo.Email, out userId))
                    {
                        modelo.DesmarcarBorradoUsuarioId = userId;
                    }
                    else
                    {
                        var usuarioIdentity = new ApplicationUser { UserName = modelo.Email, Email = modelo.Email };

                        var resultado = UserManager.Create(usuarioIdentity);
                        ValidarIdentityResult(resultado);

                        resultado = UserManager.AddToRoles(usuarioIdentity.Id, modelo.Rol);
                        ValidarIdentityResult(resultado);

                        try
                        {
                            var usuario = _usuariosNegocio.Agregar(usuarioIdentity.Id, modelo.Nombres, modelo.Apellidos, modelo.Email, UsuarioId);
                            EnviarCorreoActivacion(usuario);
                        }
                        catch (Exception)
                        {
                            UserManager.Delete(usuarioIdentity);

                            throw;
                        }

                        ControllerHelper.CargarMensajeResultadoOk("Usuario guardado correctamente.");

                        ModelState.Clear();
                        modelo = new UsuarioViewModel();
                    }
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarUsuarioViewModel(modelo, PaginaModo.Agregar);
            return View(USUARIO_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult DesmarcarComoBorrado(string id)
        {
            _usuariosNegocio.DesmarcarComoBorrado(id);
            DesbloquearUsuario(id);

            return Json(new { success = true });
        }

        public ActionResult Editar(string id)
        {
            var usuario = _usuariosRepositorio.Obtener(id, cargarRoles: true);
            if (usuario == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var modelo = UsuariosMapper.MapearUsuarioEntidadAViewModel(usuario);
            CargarUsuarioViewModel(modelo, PaginaModo.Editar);

            return View(USUARIO_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Editar(UsuarioViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuarioEntidad = UsuariosMapper.MapearUsuarioViewModelAEntidad(modelo);

                    var usuarioIdentity = UserManager.FindById(modelo.UsuarioId);

                    IdentityResult resultado;

                    string emailAnterior = usuarioIdentity.Email;
                    bool emailEditado = !String.Equals(emailAnterior, modelo.Email, StringComparison.CurrentCultureIgnoreCase);
                    if (emailEditado)
                    {
                        usuarioIdentity.Email = usuarioIdentity.UserName = modelo.Email;
                        resultado = UserManager.Update(usuarioIdentity);
                        ValidarIdentityResult(resultado);
                    }

                    var rolAnterior = UserManager.GetRoles(usuarioIdentity.Id).FirstOrDefault();
                    bool rolEditado = !string.Equals(rolAnterior, modelo.Rol, StringComparison.CurrentCultureIgnoreCase);
                    if (rolEditado)
                    {
                        resultado = UserManager.RemoveFromRoles(usuarioIdentity.Id, rolAnterior);
                        ValidarIdentityResult(resultado);

                        resultado = UserManager.AddToRoles(usuarioIdentity.Id, modelo.Rol);
                        ValidarIdentityResult(resultado);
                    }

                    try
                    {
                        _usuariosNegocio.Actualizar(usuarioEntidad, UsuarioId);
                    }
                    catch (Exception)
                    {
                        // NOTA: "rollback manual" de datos de Identity
                        usuarioIdentity.Email = usuarioIdentity.UserName = emailAnterior;
                        resultado = UserManager.Update(usuarioIdentity);
                        ValidarIdentityResult(resultado);

                        if (rolEditado)
                        {
                            resultado = UserManager.RemoveFromRoles(usuarioIdentity.Id, modelo.Rol);
                            ValidarIdentityResult(resultado);

                            resultado = UserManager.AddToRoles(usuarioIdentity.Id, rolAnterior);
                            ValidarIdentityResult(resultado);
                        }

                        throw;
                    }

                    ControllerHelper.CargarMensajeResultadoOk("Usuario actualizado correctamente.");
                }
            }
            catch (Exception ex)
            {
                ControllerHelper.CargarMensajesError(ex.Message);
            }

            CargarUsuarioViewModel(modelo, PaginaModo.Editar);
            return View(USUARIO_VIEW_NAME, modelo);
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult ReenviarActivacion(string id)
        {
            var user = UserManager.FindById(id);
            if (user == null)
            {
                return Json(new { success = false, message = "Usuario inexistente." });
            }

            if (UserManager.IsEmailConfirmed(user.Id))
            {
                return Json(new { success = false, message = "Usuario ya activado." });
            }

            var usuario = _usuariosRepositorio.Obtener(user.Id);
            if (usuario == null)
            {
                return Json(new { success = false, message = "Usuario inexistente." });
            }

            EnviarCorreoActivacion(usuario);

            return Json(new { success = true });
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult ResetearPassword(string id)
        {
            var user = UserManager.FindById(id);
            if (user == null)
            {
                return Json(new { success = false, message = "Usuario inexistente." });
            }

            if (!UserManager.IsEmailConfirmed(user.Id))
            {
                return Json(new { success = false, message = "Usuario no activado." });
            }

            var usuario = _usuariosRepositorio.Obtener(user.Id);
            if (usuario == null)
            {
                return Json(new { success = false, message = "Usuario inexistente." });
            }

            _correosNegocio.EnviarCorreoResetearPassword(
                usuario.Email,
                nombreYApellido: usuario.ToString(),
                resetearPasswordUrl: URLHelper.GenerarRutaAbsoluta(Url.Action("ResetearPassword", "Account", new { id = user.Id, code = UserManager.GeneratePasswordResetToken(user.Id) })));

            return Json(new { success = true });
        }

        [HttpPost,
        ValidateAntiForgeryToken]
        public ActionResult Eliminar(string id)
        {
            var user = UserManager.FindById(id);
            var usuario = _usuariosRepositorio.Obtener(id);

            if (user == null && usuario == null)
            {
                return Json(new { success = false, message = "Usuario inexistente." });
            }

            //Controla que no elimine el usuario que actualmente está logeado.
            if (user != null && user.Id == User.Identity.GetUserId())
            {
                return Json(new { success = false, message = "No puede eliminar su propio usuario." });
            }

            try
            {
                _usuariosNegocio.MarcarComoBorrado(id, UsuarioId);
                BloquearUsuario(id);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "No se pudo eliminar el usuario." });
            }

            return Json(new { success = true });
        }

        #endregion Acciones

        #region Metodos

        private void CargarUsuariosViewModel(UsuariosViewModel modelo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            modelo.RolesSelectList = Helpers.ListasHelper.ObtenerRolesSelectList(_usuariosRepositorio.ObtenerRoles());
        }

        private void CargarUsuarioViewModel(UsuarioViewModel modelo, PaginaModo paginaModo)
        {
            Validador.ValidarArgumentRequeridoYThrow(modelo, nameof(modelo));

            modelo.PaginaModo = paginaModo;
            modelo.RolesSelectList = Helpers.ListasHelper.ObtenerRolesSelectList(_usuariosRepositorio.ObtenerRoles());
        }

        private void EnviarCorreoActivacion(Usuario usuario)
        {
            _correosNegocio.EnviarCorreoActivarCuenta(
                usuario.Email,
                usuario.NombresYApellidos,
                activarCuentaUrl: URLHelper.GenerarRutaAbsoluta(Url.Action("ActivarCuenta", "Account", new { id = usuario.Id, code = UserManager.GenerateEmailConfirmationToken(usuario.Id) })));
        }

        private void BloquearUsuario(string usuarioId)
        {
            var resultado = UserManager.SetLockoutEndDate(usuarioId, new DateTimeOffset(new DateTime(2100, 1, 1)));
            ValidarIdentityResult(resultado);
        }

        private void DesbloquearUsuario(string usuarioId)
        {
            var resultado = UserManager.SetLockoutEndDate(usuarioId, new DateTimeOffset(new DateTime(2000, 1, 1)));
            ValidarIdentityResult(resultado);
        }

        private void ValidarIdentityResult(IdentityResult resultado)
        {
            if (!resultado.Succeeded)
            {
                throw new Exception(String.Join(Environment.NewLine, resultado.Errors));
            }
        }

        #endregion Metodos
    }
}