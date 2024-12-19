using namasdev.Core.Validation;
using namasdev.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace namasdev.Apps.Web.Portal.ViewModels.Usuarios
{
    public class UsuarioViewModel : IValidatableObject
    {
        public PaginaModo PaginaModo { get; set; }

        public string UsuarioId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        [Display(Name = "Correo electrónico"),
        EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Rol")]
        public string Rol { get; set; }
     
        public string DesmarcarBorradoUsuarioId { get; set; }

        public SelectList RolesSelectList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && String.IsNullOrWhiteSpace(UsuarioId))
            {
                yield return new ValidationResult(Validador.MensajeRequerido("Usuario"), new string[] { nameof(UsuarioId) });
            }
            if (String.IsNullOrWhiteSpace(Nombres))
            {
                yield return new ValidationResult(Validador.MensajeRequerido("Nombres"), new string[] { nameof(Nombres) });
            }
            if (String.IsNullOrWhiteSpace(Apellidos))
            {
                yield return new ValidationResult(Validador.MensajeRequerido("Apellidos"), new string[] { nameof(Apellidos) });
            }
            if (String.IsNullOrWhiteSpace(Email))
            {
                yield return new ValidationResult(Validador.MensajeRequerido("Correo electrónico"), new string[] { nameof(Email) });
            }
            if (String.IsNullOrWhiteSpace(Rol))
            {
                yield return new ValidationResult(Validador.MensajeRequerido("Rol"), new string[] { nameof(Rol) });
            }
        }
    }
}