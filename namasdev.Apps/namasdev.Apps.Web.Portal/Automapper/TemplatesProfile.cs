using AutoMapper;

using namasdev.Apps.Negocio.DTO.GeneradorArchivos;
using namasdev.Apps.Web.Portal.ViewModels.Templates;

namespace namasdev.Apps.Web.Portal.Automapper
{
    public class TemplatesProfile : Profile
    {
        public TemplatesProfile()
        {
            CreateMap<GenerarArchivosViewModel, GenerarZipParametros>();
        }
    }
}