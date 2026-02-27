using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using namasdev.Apps.Entidades.Metadata;
using namasdev.Core.Validation;

namespace namasdev.Apps.Web.Portal.ViewModels.Templates
{
    public class GenerarArchivosViewModel
    {
        public Guid Id { get; set; }

        public bool ModoAplicacion { get; set; }
        public bool ModoDebug { get; set; }

        [Display(Name = "Tabla")]
        public bool GenerarBDTabla { get; set; }

        [Display(Name = "Repositorio")]
        public bool GenerarDatosRepositorio { get; set; }

        [Display(Name = "SQL Config")]
        public bool GenerarDatosSqlConfig { get; set; }

        [Display(Name = "Entidad")]
        public bool GenerarEntidadesEntidad { get; set; }

        [Display(Name = "Metadata Entidad")]
        public bool GenerarEntidadesMetadataEntidadMetadata { get; set; }

        [Display(Name = "Negocio")]
        public bool GenerarNegocio { get; set; }

        [Display(Name = "Parámetros Agregar")]
        public bool GenerarNegocioDTOAgregarParametros { get; set; }

        [Display(Name = "Parámetros Actualizar")]
        public bool GenerarNegocioDTOActualizarParametros { get; set; }

        [Display(Name = "Parámetros MarcarComoBorrado")]
        public bool GenerarNegocioDTOMarcarComoBorradoParametros { get; set; }

        [Display(Name = "Parámetros DesmarcarComoBorrado")]
        public bool GenerarNegocioDTODesmarcarComoBorradoParametros { get; set; }

        [Display(Name = "Parámetros Eliminar")]
        public bool GenerarNegocioDTOEliminarParametros { get; set; }

        [Display(Name = "Profile Automapper")]
        public bool GenerarNegocioAutomapperProfile { get; set; }

        [Display(Name = "Controller")]
        public bool GenerarWebController { get; set; }

        [Display(Name = "Model de item")]
        public bool GenerarWebModelsItemModel { get; set; }

        [Display(Name = "ViewModel de entidad")]
        public bool GenerarWebViewModelsEntidadViewModel { get; set; }

        [Display(Name = "ViewModel de lista")]
        public bool GenerarWebViewModelsListaViewModel { get; set; }

        [Display(Name = "Profile Automapper")]
        public bool GenerarWebAutomapperProfile { get; set; }

        [Display(Name = "Metadata de Views")]
        public bool GenerarWebMetadataViews { get; set; }

        [Display(Name = "View Index")]
        public bool GenerarWebViewsIndex { get; set; }

        [Display(Name = "View Entidad")]
        public bool GenerarWebViewsEntidad { get; set; }

        public void MarcarTodos()
        {
            GenerarBDTabla =
            GenerarDatosRepositorio =
            GenerarDatosSqlConfig =
            GenerarEntidadesEntidad =
            GenerarEntidadesMetadataEntidadMetadata =
            GenerarNegocio =
            GenerarNegocioDTOAgregarParametros =
            GenerarNegocioDTOActualizarParametros =
            GenerarNegocioDTOMarcarComoBorradoParametros =
            GenerarNegocioDTODesmarcarComoBorradoParametros =
            GenerarNegocioDTOEliminarParametros =
            GenerarNegocioAutomapperProfile =
            GenerarWebController =
            GenerarWebModelsItemModel =
            GenerarWebViewModelsEntidadViewModel =
            GenerarWebViewModelsListaViewModel =
            GenerarWebAutomapperProfile =
            GenerarWebMetadataViews =
            GenerarWebViewsIndex =
            GenerarWebViewsEntidad =
                true;
        }

        [Display(Name = AplicacionVersionMetadata.ETIQUETA),
        Required(ErrorMessage = Validador.REQUERIDO_TEXTO_FORMATO)]
        public Guid? AplicacionVersionId { get; set; }

        public SelectList VersionesSelectList { get; set; }
    }
}