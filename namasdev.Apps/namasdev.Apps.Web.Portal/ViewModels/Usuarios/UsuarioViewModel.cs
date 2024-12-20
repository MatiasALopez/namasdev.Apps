using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Models;
using namasdev.Apps.Entidades.Metadata;

namespace namasdev.Apps.Web.Portal.ViewModels.Usuarios
{
    public class UsuarioViewModel : IValidatableObject
    {
        public PaginaModo PaginaModo { get; set; }

        public string UsuarioId { get; set; }

        [Display(Name = UsuarioMetadata.Nombres.DISPLAY_NAME)]
        public string Nombres { get; set; }

        [Display(Name = UsuarioMetadata.Apellidos.DISPLAY_NAME)]
        public string Apellidos { get; set; }

        [Display(Name = UsuarioMetadata.Email.DISPLAY_NAME),
        EmailAddress]
        public string Email { get; set; }

        [Display(Name = AspNetRoleMetadata.NOMBRE)]
        public string Rol { get; set; }
     
        public string DesmarcarBorradoUsuarioId { get; set; }

        public SelectList RolesSelectList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && String.IsNullOrWhiteSpace(UsuarioId))
            {
                yield return new ValidationResult(Validador.MensajeRequerido(UsuarioMetadata.NOMBRE), new string[] { nameof(UsuarioId) });
            }
            if (String.IsNullOrWhiteSpace(Nombres))
            {
                yield return new ValidationResult(Validador.MensajeRequerido(UsuarioMetadata.Nombres.DISPLAY_NAME), new string[] { nameof(Nombres) });
            }
            if (String.IsNullOrWhiteSpace(Apellidos))
            {
                yield return new ValidationResult(Validador.MensajeRequerido(UsuarioMetadata.Apellidos.DISPLAY_NAME), new string[] { nameof(Apellidos) });
            }
            if (String.IsNullOrWhiteSpace(Email))
            {
                yield return new ValidationResult(Validador.MensajeRequerido(UsuarioMetadata.Email.DISPLAY_NAME), new string[] { nameof(Email) });
            }
            if (String.IsNullOrWhiteSpace(Rol))
            {
                yield return new ValidationResult(Validador.MensajeRequerido(AspNetRoleMetadata.NOMBRE), new string[] { nameof(Rol) });
            }
        }
    }
}