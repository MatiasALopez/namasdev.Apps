
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Negocio.DTO.GeneradorArchivos
{
    public class GenerarZipParametros
    {
        public Entidad Entidad { get; set; }
        public bool GenerarDatabaseTabla { get; set; }
        public bool GenerarDatosRepositorio { get; set; }
        public bool GenerarDatosSqlConfig { get; set; }
        public bool GenerarEntidadesEntidad { get; set; }
        public bool GenerarEntidadesMetadataEntidadMetadata { get; set; }
        public bool GenerarNegocio { get; set; }
        public bool GenerarNegocioDTOAgregarParametros { get; set; }
        public bool GenerarNegocioDTOActualizarParametros { get; set; }
        public bool GenerarNegocioDTOMarcarComoBorradoParametros { get; set; }
        public bool GenerarNegocioDTODesmarcarComoBorradoParametros { get; set; }
        public bool GenerarNegocioAutomapperProfile { get; set; }
        public bool GenerarWebController { get; set; }
        public bool GenerarWebModelsItemModel { get; set; }
        public bool GenerarWebViewModelsEntidadViewModel { get; set; }
        public bool GenerarWebViewModelsListaViewModel { get; set; }
        public bool GenerarWebAutomapperProfile { get; set; }
        public bool GenerarWebViewsMetadata { get; set; }
        public bool GenerarWebViewsIndex { get; set; }
        public bool GenerarWebViewsEntidad { get; set; }

        public bool GenerarAlMenosUnArchivo
        {
            get 
            {
                return GenerarDatabaseTabla
                    || GenerarDatosRepositorio
                    || GenerarDatosSqlConfig
                    || GenerarEntidadesEntidad
                    || GenerarEntidadesMetadataEntidadMetadata
                    || GenerarNegocio
                    || GenerarNegocioDTOAgregarParametros
                    || GenerarNegocioDTOActualizarParametros
                    || GenerarNegocioDTOMarcarComoBorradoParametros
                    || GenerarNegocioDTODesmarcarComoBorradoParametros
                    || GenerarNegocioAutomapperProfile
                    || GenerarWebController
                    || GenerarWebModelsItemModel
                    || GenerarWebViewModelsEntidadViewModel
                    || GenerarWebViewModelsListaViewModel
                    || GenerarWebAutomapperProfile
                    || GenerarWebViewsMetadata
                    || GenerarWebViewsIndex
                    || GenerarWebViewsEntidad;
            }
        }
    }
}
