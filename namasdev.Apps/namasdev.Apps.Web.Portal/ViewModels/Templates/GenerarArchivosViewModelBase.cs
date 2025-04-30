using System;
using System.ComponentModel.DataAnnotations;

namespace namasdev.Apps.Web.Portal.ViewModels.Templates
{
    public class GenerarArchivosViewModelBase
    {
        [Display(Name = "Tabla")]
        public bool GenerarDatabaseTabla { get; set; }

        [Display(Name = "Repositorio")]
        public bool GenerarDatosRepositorio { get; set; }

        [Display(Name = "SQL Config")]
        public bool GenerarDatosSqlConfig { get; set; }

        [Display(Name = "Entidad")]
        public bool GenerarEntidadesEntidad { get; set; }

        [Display(Name = "Metadata Entidad")]
        public bool GenerarEntidadesMetadataEntidadMetadata { get; set; }

        [Display(Name = "Repositorio")]
        public bool GenerarNegocio { get; set; }

        [Display(Name = "Parámetros Agregar")]
        public bool GenerarNegocioDTOAgregarParametros { get; set; }

        [Display(Name = "Parámetros Actualizar")]
        public bool GenerarNegocioDTOActualizarParametros { get; set; }

        [Display(Name = "Parámetros MarcarComoBorrado")]
        public bool GenerarNegocioDTOMarcarComoBorradoParametros { get; set; }

        [Display(Name = "Parámetros DesmarcarComoBorrado")]
        public bool GenerarNegocioDTODesmarcarComoBorradoParametros { get; set; }

        [Display(Name = "Profile Automapper")]
        public bool GenerarNegocioAutomapperProfile { get; set; }

        [Display(Name = "Controller")]
        public bool GenerarWebController { get; set; }

        [Display(Name = "ItemModel")]
        public bool GenerarWebModelsItemModel { get; set; }

        [Display(Name = "ViewModel de entidad")]
        public bool GenerarWebViewModelsEntidadViewModel { get; set; }

        [Display(Name = "ViewModel de lista")]
        public bool GenerarWebViewModelsListaViewModel { get; set; }

        [Display(Name = "Profile Automapper")]
        public bool GenerarWebAutomapperProfile { get; set; }

        [Display(Name = "Metadata de Views")]
        public bool GenerarWebViewsMetadata { get; set; }

        [Display(Name = "Index")]
        public bool GenerarWebViewsIndex { get; set; }

        [Display(Name = "Entidad")]
        public bool GenerarWebViewsEntidad { get; set; }

        public void MarcarTodos()
        {
            GenerarDatabaseTabla =
            GenerarDatosRepositorio =
            GenerarDatosSqlConfig =
            GenerarEntidadesEntidad =
            GenerarEntidadesMetadataEntidadMetadata =
            GenerarNegocio =
            GenerarNegocioDTOAgregarParametros =
            GenerarNegocioDTOActualizarParametros =
            GenerarNegocioDTOMarcarComoBorradoParametros =
            GenerarNegocioDTODesmarcarComoBorradoParametros =
            GenerarNegocioAutomapperProfile =
            GenerarWebController =
            GenerarWebModelsItemModel =
            GenerarWebViewModelsEntidadViewModel =
            GenerarWebViewModelsListaViewModel =
            GenerarWebAutomapperProfile =
            GenerarWebViewsMetadata =
            GenerarWebViewsIndex =
            GenerarWebViewsEntidad =
                true;
        }
    }
}