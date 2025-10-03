using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

using namasdev.Core.Validation;
using namasdev.Web.Models;

using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Web.Portal.Models.EntidadesPropiedades;

namespace namasdev.Apps.Web.Portal.ViewModels.EntidadesIndices
{
    public class EntidadIndiceViewModel : IValidatableObject
    {
        public PaginaModo PaginaModo { get; set; }

        public Guid AplicacionVersionId { get; set; }
        public Guid EntidadId { get; set; }
        public string EntidadTablaNombre { get; set; }

        [Display(Name = EntidadIndiceMetadata.Propiedades.Id.ETIQUETA)]
        public Guid? Id { get; set; }
        
        [Display(Name = EntidadIndiceMetadata.Propiedades.Nombre.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public string Nombre { get; set; }
        
        [Display(Name = EntidadIndiceMetadata.Propiedades.EsUnique.ETIQUETA)]
        [Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public bool? EsUnique { get; set; }

        [Display(Name = EntidadIndiceMetadata.Propiedades.Condiciones.ETIQUETA)]
        public string Condiciones { get; set; }

        public List<EntidadPropiedadItemModel> Propiedades { get; set; }

        public Guid[] PropiedadesIdsSeleccionadas
        {
            get 
            {
                return Propiedades?
                    .Where(p => p.Seleccionado)
                    .Select(p => p.Id)
                    .ToArray();
            }
        }

        public SelectList SiNoSelectList { get; set; }

        public void SeleccionarPropiedades(IEnumerable<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                return;
            }

            foreach (var p in Propiedades)
            {
                p.Seleccionado = ids.Contains(p.Id);
            }
        }
    
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaginaModo == PaginaModo.Editar
                && !Id.HasValue)
            {
                yield return new ValidationResult(Validador.MensajeRequerido(EntidadIndiceMetadata.ETIQUETA));
            }

            if (PropiedadesIdsSeleccionadas == null || !PropiedadesIdsSeleccionadas.Any())
            {
                yield return new ValidationResult("Debe seleccionar al menos una propiedad.");
            }
        }
    }
}
